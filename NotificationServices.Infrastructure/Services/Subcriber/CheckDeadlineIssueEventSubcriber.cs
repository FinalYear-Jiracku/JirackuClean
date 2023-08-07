
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationServices.Domain.Common.Interfaces;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using NotificationServices.Application.Messages;
using NotificationServices.Application.Interfaces.IServices;

namespace NotificationServices.Infrastructure.Services.Subcriber
{
    public class CheckDeadlineIssueEventSubcriber : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IEmailService _emailService;
        private const string ExchangeName = "deadline_issue_exchange";
        private const string QueueName = "DeadlineIssue";

        public CheckDeadlineIssueEventSubcriber(IEmailService emailService)
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
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                List<DeadlineIssue> issueEvent = JsonConvert.DeserializeObject<List<DeadlineIssue>>(message);
                HandleMessage(issueEvent);
            };
            _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
        private async Task HandleMessage(List<DeadlineIssue> deadllineIssues)
        {
            await _emailService.SendEmailDeadlineIssue(deadllineIssues);
        }
    }
}
