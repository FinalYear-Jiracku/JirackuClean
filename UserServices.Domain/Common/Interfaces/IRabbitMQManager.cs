using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Domain.Common.Interfaces
{
    public interface IRabbitMQManager
    {
        IModel CreateModel();
        void CloseConnection();
        void CheckConnection();
    }
}
