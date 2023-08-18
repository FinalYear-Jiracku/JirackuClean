using NotificationServices.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Interfaces.IServices
{
    public interface IEventService
    {
        Task SendEvent(List<EventCalendar> request);
    }
}
