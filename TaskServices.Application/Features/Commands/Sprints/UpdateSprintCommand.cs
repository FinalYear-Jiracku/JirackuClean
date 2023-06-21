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
    public class UpdateSprintCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ProjectId { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class SprintUpdatedEvent : BaseEvent
    {
        public Sprint Sprint { get; }

        public SprintUpdatedEvent(Sprint sprint)
        {
            Sprint = sprint;
        }
    }
}
