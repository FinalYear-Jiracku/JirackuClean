using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Domain.Common.Interfaces
{
    public interface INotificationEventPulisher
    {
        void SendMessage(string email, int projectId, string inviteToken);
        void Dispose();
    }
}
