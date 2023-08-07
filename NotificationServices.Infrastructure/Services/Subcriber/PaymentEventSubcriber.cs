using Microsoft.Extensions.Hosting;
using NotificationServices.Application.Interfaces.IServices;
using NotificationServices.Application.Messages;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NotificationServices.Infrastructure.Services.Subcriber
{
    public class PaymentEventSubcriber : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IEmailService _emailService;
        private const string ExchangeName = "payment_exchange";
        private const string QueueName = "payment";

        public PaymentEventSubcriber(IEmailService emailService)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
                VirtualHost = "/"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
            _channel.QueueDeclare(QueueName, false, false, false, null);
            _channel.QueueBind(QueueName, ExchangeName, "");
            _emailService = emailService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                PaymentProject paymentEvent = JsonConvert.DeserializeObject<PaymentProject>(message);
                await HandleMessage(paymentEvent);
            };
            _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(PaymentProject paymentProject)
        {
            await _emailService.SendEmailPayment(paymentProject);
        }
    }
}
