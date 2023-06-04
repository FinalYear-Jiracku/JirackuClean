using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Sprints
{
    public class CreateSprintCommand : IRequest<Sprint>
    {
        public string? Name { get; set; }
        public int? ProjectId { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class SprintCreatedEvent : BaseEvent
    {
        public Sprint Sprint { get; }

        public SprintCreatedEvent(Sprint sprint)
        {
            Sprint = sprint;
        }
    }
}
