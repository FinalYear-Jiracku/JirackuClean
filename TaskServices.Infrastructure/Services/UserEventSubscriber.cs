using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common.Interfaces;
using TaskServices.Domain.Entities;
using Newtonsoft.Json;
using TaskServices.Application.Interfaces;

namespace TaskServices.Infrastructure.Services
{
    public class UserEventSubscriber : IUserEventSubscriber
    {
        private readonly IRabbitMQManager _rabbitMQManager;
        private IModel _channel;
        private const string ExchangeName = "user_exchange";
        private readonly IUnitOfWork _unitOfWork;
        private ManualResetEvent _stopConsumeEvent = new ManualResetEvent(false);

        public UserEventSubscriber(IRabbitMQManager rabbitMQManager, IUnitOfWork unitOfWork)
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

        public async Task ReceiveMessage()
        {
            _channel.QueueDeclare("users", true, false, false, null);
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
            _channel.QueueBind("users", ExchangeName, "");

            var messageCount = _channel.QueueDeclarePassive("users").MessageCount;

            if (messageCount == 0)
            {
                Console.WriteLine("No user event messages in the queue.");
                return;
            }

            var consumer = new EventingBasicConsumer(_channel);

            var users = new List<User>();
            users.Clear();

            var consumerFinishedEvent = new ManualResetEvent(false);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                dynamic userEvent = JsonConvert.DeserializeObject(message);

                var user = new User
                {
                    Id = userEvent?.User.Id,
                    Name = userEvent?.User.Name,
                    Email = userEvent?.User.Email,
                    Image = userEvent?.User.Image,
                };
                users.Add(user);

                Console.WriteLine("Received message: " + userEvent);
                consumerFinishedEvent.Set();
            };

            var consumerTag = _channel.BasicConsume(queue: "users", autoAck: true, consumer: consumer);

            _channel.BasicCancel(consumerTag);

            consumerFinishedEvent.WaitOne();
            await _unitOfWork.UserRepository.Update(users);
        }

        public void StopConsume()
        {
            _stopConsumeEvent.Set();
        }
    }
}
