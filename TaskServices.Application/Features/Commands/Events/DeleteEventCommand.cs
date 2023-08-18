using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Events
{
    public class DeleteEventCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteEventCommand(int id)
        {
            Id = id;
        }
    }
    public class EventDeletedEvent : BaseEvent
    {
        public EventCalendar Event { get; }

        public EventDeletedEvent(EventCalendar @event)
        {
            Event = @event;
        }
    }
}
