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
    public class CreateEventCommand : IRequest<EventCalendar>
    {
        public string? Title { get; set; }
        public int? ProjectId { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class EventCreatedEvent : BaseEvent
    {
        public EventCalendar Event { get; }

        public EventCreatedEvent(EventCalendar @event)
        {
            Event = @event;
        }
    }
}
