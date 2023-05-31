using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.Features.Queries.Projects
{
    public class CheckProjectNameQuery : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public CheckProjectNameQuery(string? name)
        {
            Name = name;
        }
        public CheckProjectNameQuery(int id,string? name)
        {
            Id = id;
            Name = name;
        }
    }
}
