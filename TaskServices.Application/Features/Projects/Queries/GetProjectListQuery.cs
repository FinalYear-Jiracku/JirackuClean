using KPTMockProject.Common.Filter;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Projects.Queries
{
    public class GetProjectListQuery : IRequest<(List<ProjectDTO>, PaginationFilter, int)>
    {
        public PaginationFilter? Filter { get; set; }
        public GetProjectListQuery(PaginationFilter filter)
        {
            Filter = filter;
        }
    }
}
