using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.Interfaces.IServices
{
    public interface INotificationEventSubcriber
    {
        Task ReceiveMessage(string queueName, string exchangeName);
        void Dispose();
        void StopConsume();
    }
}
