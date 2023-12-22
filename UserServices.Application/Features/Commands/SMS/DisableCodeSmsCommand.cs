﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Application.Features.Commands.SMS
{
    public class DisableCodeSmsCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public DisableCodeSmsCommand(int userId)
        {
            UserId = userId;
        }
    }
}
