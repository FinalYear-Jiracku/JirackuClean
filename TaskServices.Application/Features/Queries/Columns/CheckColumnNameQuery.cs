using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.Features.Queries.Columns
{
    public class CheckColumnNameQuery : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public CheckColumnNameQuery(string? name)
        {
            Name = name;
        }
        public CheckColumnNameQuery(int id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
}
