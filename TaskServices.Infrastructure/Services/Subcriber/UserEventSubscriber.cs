using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using TaskServices.Domain.Entities;
using Newtonsoft.Json;
using TaskServices.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using TaskServices.Domain.Common.Interfaces;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Infrastructure.Services.Subcriber
{
    public class UserEventSubscriber : IUserEventSubscriber
    {
        private readonly IRabbitMQManager _rabbitMQManager;
        private IModel _channel;
        private readonly IUnitOfWork _unitOfWork;
        private ManualResetEvent _stopConsumeEvent = new ManualResetEvent(false);
        private const string ExchangeName = "user_exchange";
        private const string QueueName = "users";

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
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
            _channel.QueueDeclare(QueueName, false, false, false, null);
            _channel.QueueBind(QueueName, ExchangeName, "");

            var messageCount = _channel.QueueDeclarePassive(QueueName).MessageCount;

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
                    Name = userEvent?.User.Name,
                    Email = userEvent?.User.Email,
                    Image = userEvent?.User.Image,
                    Role = userEvent?.User.Role,
                };
                users.Add(user);

                Console.WriteLine("Received message: " + userEvent);
                consumerFinishedEvent.Set();
            };

            var consumerTag = _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

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
