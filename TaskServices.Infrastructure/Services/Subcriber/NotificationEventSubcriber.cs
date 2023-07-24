using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using Newtonsoft.Json;

namespace TaskServices.Infrastructure.Services.Subcriber
{
    public class NotificationEventSubcriber : INotificationEventSubcriber
    {
        private readonly IRabbitMQManager _rabbitMQManager;
        private IModel _channel;
        private readonly IUnitOfWork _unitOfWork;
        private ManualResetEvent _stopConsumeEvent = new ManualResetEvent(false);

        public NotificationEventSubcriber(IRabbitMQManager rabbitMQManager, IUnitOfWork unitOfWork)
        {
            _rabbitMQManager = rabbitMQManager;
            _channel = rabbitMQManager.CreateModel();
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _rabbitMQManager?.CloseConnection();
        }

        public async Task ReceiveMessage(string queueName, string exchangeName)
        {
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false, true, null);
            _channel.QueueDeclare(queueName, false, false, true, null);
            _channel.QueueBind(queueName, exchangeName, "");

            var messageCount = _channel.QueueDeclarePassive(queueName).MessageCount;

            if (messageCount == 0)
            {
                Console.WriteLine("No user event messages in the queue.");
                return;
            }

            var consumer = new EventingBasicConsumer(_channel);

            var users = new List<User>();
            var userProjects = new List<UserProject>();
            users.Clear();

            var consumerFinishedEvent = new ManualResetEvent(false);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                dynamic userEvent = JsonConvert.DeserializeObject(message);

                var inviteTokenFromMessage = userEvent?.inviteToken?.ToString();

                var user = new User
                {
                    Email = userEvent?.email?.ToString()
                };
                users.Add(user);
                await _unitOfWork.UserRepository.Update(users);
                foreach (var userToAdd in users)
                {
                    var findUser = _unitOfWork.UserRepository.FindUserByEmail(userToAdd.Email).Result;
                    var userProject = new UserProject()
                    {
                        UserId = findUser.Id,
                        ProjectId = userEvent?.projectId
                    };
                    userProjects.Add(userProject);
                }
                await _unitOfWork.UserRepository.UpdateUserProjectList(userProjects);
                Console.WriteLine("Received message: " + userEvent);
                consumerFinishedEvent.Set();
            };
            var consumerTag = _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            _channel.BasicCancel(consumerTag);
            consumerFinishedEvent.WaitOne();
        }

        public void StopConsume()
        {
            _stopConsumeEvent.Set();
        }
    }
}
