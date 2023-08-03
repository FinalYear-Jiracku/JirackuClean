using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Queries.Issues
{
    public class StatisTypeQuery : IRequest<StatisTypeDTO>
    {
        public int Id { get; set; }
        public StatisTypeQuery(int id)
        {
            Id = id;
        }
    }
}
