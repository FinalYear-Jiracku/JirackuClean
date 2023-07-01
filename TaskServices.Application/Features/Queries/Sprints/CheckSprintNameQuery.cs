﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.Features.Queries.Sprints
{
    public class CheckSprintNameQuery : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ProjectId { get; set; }
        public CheckSprintNameQuery(string? name, int? projectId)
        {
            Name = name;
            ProjectId = projectId;
        }
        public CheckSprintNameQuery(int id, string? name, int? projectId)
        {
            Id = id;
            Name = name;
            ProjectId = projectId;
        }
    }
}
