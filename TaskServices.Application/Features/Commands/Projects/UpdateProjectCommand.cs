using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Projects
{
    public class UpdateProjectCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public UpdateProjectCommand(int id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
    public class ProjectUpdatedEvent : BaseEvent
    {
        public Project Project { get; }

        public ProjectUpdatedEvent(Project project)
        {
            Project = project;
        }
    }
}
