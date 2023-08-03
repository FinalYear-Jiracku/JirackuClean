using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Issues
{
    public class StatisStatusQuery : IRequest<StatisStatusDTO>
    {
        public int Id { get; set; }
        public StatisStatusQuery(int id)
        {
            Id = id;
        }
    }
}
