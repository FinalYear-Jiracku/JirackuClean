using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Domain.Entities;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Queries.Projects
{
    public class GetProjectListQuery : IRequest<(List<ProjectDTO>, PaginationFilter, int)>
    {
        public PaginationFilter? Filter { get; set; }
        public string? Email { get; set; }
        public GetProjectListQuery(PaginationFilter filter, string? email)
        {
            Filter = filter;
            Email = email;
        }
    }
}
