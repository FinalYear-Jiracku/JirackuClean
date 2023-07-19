using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Domain.Entities;

namespace UserServices.Application.Features.Queries
{
    public class GetProfileQueries : IRequest<UserDTO>
    {
        public int Id { get; set; }
        public GetProfileQueries(int id)
        {
            Id = id;
        }
    }
}
