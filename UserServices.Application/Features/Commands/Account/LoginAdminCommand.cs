using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Domain.Common;
using UserServices.Domain.Entities;

namespace UserServices.Application.Features.Commands.Account
{
    public class LoginAdminCommand : IRequest<AuthResponseDTO>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class AdminLoggedInEvent : BaseEvent
    {
        public User? User { get; }
        public AuthResponseDTO? Auth { get; }
        public AdminLoggedInEvent(User user)
        {
            User = user;
        }
    }
}
