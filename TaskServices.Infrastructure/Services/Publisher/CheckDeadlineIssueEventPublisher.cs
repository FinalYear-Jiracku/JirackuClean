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
using TaskServices.Domain.Entities;

namespace TaskServices.Infrastructure.Services.Publisher
{
    public class CheckDeadlineIssueEventPublisher : ICheckDeadlineIssueEventPublisher
    {
        private readonly IRabbitMQManager _rabbitMQManager;
        private IModel _channel;
        private const string ExchangeName = "deadline_issue_exchange";
        private const string QueueName = "DeadlineIssue";

        public CheckDeadlineIssueEventPublisher(IRabbitMQManager rabbitMQManager)
        {
            _rabbitMQManager = rabbitMQManager;
            _channel = rabbitMQManager.CreateModel();
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _rabbitMQManager?.CloseConnection();
        }

        public void SendMessage(List<DeadlineIssuesDTO> issues)
        {
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
            _channel.QueueDeclare(QueueName, false, false, false, null);
            _channel.QueueBind(QueueName, ExchangeName, "");
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(issues));
            _channel.BasicPublish(ExchangeName, "" , null, body);
        }
    }
}
