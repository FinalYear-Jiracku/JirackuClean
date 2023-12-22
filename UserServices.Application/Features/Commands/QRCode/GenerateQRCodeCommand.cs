using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;

namespace UserServices.Application.Features.Commands.QRCode
{
    public class GenerateQRCodeCommand : IRequest<QRCodeDTO>
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
    }
}
