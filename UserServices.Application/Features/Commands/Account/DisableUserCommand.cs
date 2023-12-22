using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Domain.Common;
using UserServices.Domain.Entities;

namespace UserServices.Application.Features.Commands.Account
{
    public class DisableUserCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DisableUserCommand(int id)
        {
            Id = id;
        }
    }
}
