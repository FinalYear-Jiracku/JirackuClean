using Newtonsoft.Json;
using NotificationServices.Domain.Common;
using NotificationServices.Domain.Common.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NotificationServices.Infrastructure.Services
{
    public class NotificationEventPublisher : INotificationEventPulisher
    {
        private readonly IRabbitMQManager _rabbitMQManager;
        private IModel _channel;
        

        public NotificationEventPublisher(IRabbitMQManager rabbitMQManager)
        {
            _rabbitMQManager = rabbitMQManager;
            _channel = rabbitMQManager.CreateModel();
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _rabbitMQManager?.CloseConnection();
        }

        public void SendMessage(string email, int projectId, string inviteToken)
        {
            var ExchangeName = $"{inviteToken}";
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout, false, true, null);
            _channel.QueueDeclare(inviteToken, false, false, true, null);
            _channel.QueueBind(inviteToken, ExchangeName, "");
            var message = new { email = email, projectId = projectId, inviteToken = inviteToken };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            _channel.BasicPublish(ExchangeName, inviteToken, null, body);
        }
    }
}
