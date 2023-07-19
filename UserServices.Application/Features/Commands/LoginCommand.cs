using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Domain.Common;
using UserServices.Domain.Entities;

namespace UserServices.Application.Features.Commands
{
    public class LoginCommand : IRequest<AuthResponseDTO>
    {
        public string? TokenId { get; set; }
    }

    public class UserLoggedInEvent : BaseEvent
    {
        public User? User { get; }
        public AuthResponseDTO? Auth { get; }
        public UserLoggedInEvent(User user)
        {
            User = user;
        }
        public UserLoggedInEvent(AuthResponseDTO auth)
        {
            Auth = auth;
        }
    }
}
