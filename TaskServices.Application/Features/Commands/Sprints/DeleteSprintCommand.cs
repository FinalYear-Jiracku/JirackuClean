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
    public class DeleteSprintCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DeleteSprintCommand(int id, int projectId)
        {
            Id = id;
            ProjectId = projectId;
        }
    }
    public class SprintDeletedEvent : BaseEvent
    {
        public Sprint Sprint { get; }

        public SprintDeletedEvent(Sprint sprint)
        {
            Sprint = sprint;
        }
    }
}
