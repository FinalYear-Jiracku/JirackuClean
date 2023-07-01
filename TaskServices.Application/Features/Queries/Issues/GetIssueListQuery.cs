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
    public class GetIssueListQuery : IRequest<(List<IssueDTO>, PaginationFilter, int)>
    {
        public int Id { get; set; }
        public PaginationFilter Filter { get; set; }
        public GetIssueListQuery(int id, PaginationFilter filter)
        {
            Id = id;
            Filter = filter;
        }
    }
}
