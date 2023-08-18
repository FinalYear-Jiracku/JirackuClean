using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Events
{
    public class AuthZoomCommand : IRequest<string>
    {
        public string? Code { get; set; }
        public AuthZoomCommand(string? code)
        {
            Code = code;
        }
    }
}
