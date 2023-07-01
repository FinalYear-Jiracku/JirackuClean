using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Queries.Sprints
{
    public class GetSprintListQuery : IRequest<(List<SprintDTO>, PaginationFilter, int)>
    {
        public int Id { get; set; }
        public PaginationFilter? Filter { get; set; }
        public GetSprintListQuery(PaginationFilter filter, int id)
        {
            Filter = filter;
            Id = id;
        }
    }
}
