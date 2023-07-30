using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.Features.Queries.Statuses
{
    public class CheckStatusNameQuery : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? SprintId { get; set; }
        public CheckStatusNameQuery(string? name, int? sprintId)
        {
            Name = name;
            SprintId = sprintId;
        }
        public CheckStatusNameQuery(int id, string? name, int? sprintId)
        {
            Id = id;
            Name = name;
            SprintId = sprintId;
        }
    }
}
