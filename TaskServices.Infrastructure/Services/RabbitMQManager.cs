using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common.Interfaces;

namespace TaskServices.Infrastructure.Services
{
    public class RabbitMQManager : IRabbitMQManager
    {
        private IModel _channel;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;

        public RabbitMQManager()
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
            };
            CheckConnection();
        }

        public IModel CreateModel()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                //    //throw new Exception("RabbitMQ connection has not started or closed.");
                Thread.Sleep(1000);
                _connection = _connectionFactory.CreateConnection();
                //CheckConnection();

            }

            return _connection.CreateModel();
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.IsOpen)
                _connection.Close();
        }

        public void CheckConnection()
        {
            while (true)
            {
                try
                {
                    using var connection = _connectionFactory.CreateConnection();
                    _connection = connection;
                    break;
                }
                catch (ApplicationException)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
