using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Common.Interfaces;

namespace TaskServices.Infrastructure.Services.Publisher
{
    public class PaymentEventPublisher : IPaymentEventPublisher
    {
        private readonly IRabbitMQManager _rabbitMQManager;
        private IModel _channel;
        private const string ExchangeName = "payment_exchange";
        private const string QueueName = "payment";

        public PaymentEventPublisher(IRabbitMQManager rabbitMQManager)
        {
            _rabbitMQManager = rabbitMQManager;
            _channel = rabbitMQManager.CreateModel();
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _rabbitMQManager?.CloseConnection();
        }

        public void SendMessage(PaymentProjectDTO paymentProjectDTO)
        {
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
            _channel.QueueDeclare(QueueName, false, false, false, null);
            _channel.QueueBind(QueueName, ExchangeName, "");
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(paymentProjectDTO));
            _channel.BasicPublish(ExchangeName, "", null, body);
        }
    }
}
