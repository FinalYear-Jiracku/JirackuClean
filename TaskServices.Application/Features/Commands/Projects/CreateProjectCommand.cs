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
    public class CreateProjectCommand : IRequest<Project>
    {
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class ProjectCreatedEvent : BaseEvent
    {
        public Project Project { get; }

        public ProjectCreatedEvent(Project project)
        {
            Project = project;
        }
    }
}
