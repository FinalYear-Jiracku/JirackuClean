using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;

namespace UserServices.Application.Features.Commands
{
    public class ValidateQRCodeCommand : IRequest<ValidateQRCodeDTO>
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
    }
}
