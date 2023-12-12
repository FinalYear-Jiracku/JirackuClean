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
    public class GetProfileQuery : IRequest<UserDTO>
    {
        public int Id { get; set; }
        public GetProfileQuery(int id)
        {
            Id = id;
        }
    }
}
