﻿using Newtonsoft.Json;
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
    public class CheckDeadlineSubIssueEventPublisher : ICheckDeadlineSubIssueEventPublisher
    {
        private readonly IRabbitMQManager _rabbitMQManager;
        private IModel _channel;
        private const string ExchangeName = "deadline_subIssue_exchange";
        private const string QueueName = "DeadlineSubIssue";

        public CheckDeadlineSubIssueEventPublisher(IRabbitMQManager rabbitMQManager)
        {
            _rabbitMQManager = rabbitMQManager;
            _channel = rabbitMQManager.CreateModel();
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _rabbitMQManager?.CloseConnection();
        }

        public void SendMessage(List<DeadlineSubIssuesDTO> subIssues)
        {
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
            _channel.QueueDeclare(QueueName, false, false, false, null);
            _channel.QueueBind(QueueName, ExchangeName, "");
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(subIssues));
            _channel.BasicPublish(ExchangeName, "", null, body);
        }
    }
}
