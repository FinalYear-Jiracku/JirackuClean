using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Domain.Entities;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Queries.Statuses
{
    public class GetDataStatusListQuery : IRequest<(List<DataStatusDTO>, PaginationFilter, int)>
    {
        public int Id { get; set; }
        public PaginationFilter Filter { get; set; }
        public GetDataStatusListQuery(int id, PaginationFilter filter)
        {
            Id = id;
            Filter = filter;
        }
    }
}
