using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Interfaces.IServices
{
    public interface ICheckEventPublisher
    {
        void SendMessage(List<EventCalendarDTO> events);
        void Dispose();
    }
}
