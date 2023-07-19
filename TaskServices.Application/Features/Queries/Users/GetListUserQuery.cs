using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Domain.Entities;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Queries.Users
{
    public class GetListUserQuery : IRequest<List<UserDTO>>
    {
        public int Id { get; set; }
        public GetListUserQuery(int id)
        {
            Id = id;
        }
    }
}
