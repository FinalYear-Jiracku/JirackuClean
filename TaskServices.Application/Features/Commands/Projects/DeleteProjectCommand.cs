using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Projects
{
    public class DeleteProjectCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteProjectCommand(int id)
        {
            Id = id;
        }
    }
    public class ProjectDeletedEvent : BaseEvent
    {
        public Project Project { get; }

        public ProjectDeletedEvent(Project project)
        {
            Project = project;
        }
    }
}
