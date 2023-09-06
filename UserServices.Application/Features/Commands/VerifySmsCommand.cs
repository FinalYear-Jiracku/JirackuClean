using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;

namespace UserServices.Application.Features.Commands
{
    public class VerifySmsCommand : IRequest<VerifySmsDTO>
    {
        public int UserId { get; set; }
        public string? Phone { get; set; }
        public string? Code { get; set; }
    }
}
