﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;
using TaskServices.Domain.Entities.Enums;

namespace TaskServices.Application.Features.Commands.Projects
{
    public class UpdateProjectCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
        public UpdateProjectCommand(int id, string? name, string? updatedBy, DateTimeOffset? updatedAt)
        {
            Id = id;
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
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