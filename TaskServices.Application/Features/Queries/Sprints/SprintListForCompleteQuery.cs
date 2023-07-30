using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Sprints
{
    public class SprintListForCompleteQuery : IRequest<List<SprintDTO>>
    {
        public int Id { get; set; }
        public int SprintId { get; set; }
        public SprintListForCompleteQuery(int id, int sprintId)
        {
            Id = id;
            SprintId = sprintId;
        }
    }
}
