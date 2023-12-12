using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Shared.Pagination.Filter;

namespace UserServices.Application.Features.Queries
{
    public class GetListUserQuery : IRequest<(List<UserDTO>, PaginationFilter, int)>
    {
        public PaginationFilter? Filter { get; set; }
        public string? Email { get; set; }
        public GetListUserQuery(PaginationFilter filter)
        {
            Filter = filter;
        }
    }
}
