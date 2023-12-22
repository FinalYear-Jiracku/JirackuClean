using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;

namespace UserServices.Application.Features.Commands.QRCode
{
    public class DisableOTPCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public DisableOTPCommand(int userId)
        {
            UserId = userId;
        }
    }
}
