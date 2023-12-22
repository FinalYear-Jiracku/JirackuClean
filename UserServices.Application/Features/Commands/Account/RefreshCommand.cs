using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Domain.Common;
namespace UserServices.Application.Features.Commands.Account
{
    public class RefreshCommand : IRequest<AuthResponseDTO>
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }

    public class UserRefredhedEvent : BaseEvent
    {
        public AuthResponseDTO? Auth { get; }
        public UserRefredhedEvent(AuthResponseDTO auth)
        {
            Auth = auth;
        }
    }
}
