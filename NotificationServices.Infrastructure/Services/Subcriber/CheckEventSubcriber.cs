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
    public class CheckEventSubcriber : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IEventService _eventService;
        private const string ExchangeName = "event_exchange";
        private const string QueueName = "Event";

        public CheckEventSubcriber(IEventService eventService)
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
            _eventService = eventService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                List<EventCalendar> events = JsonConvert.DeserializeObject<List<EventCalendar>>(message);
                HandleMessage(events);
            };
            _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
        private async Task HandleMessage(List<EventCalendar> events)
        {
            await _eventService.SendEvent(events);
        }
    }
}
