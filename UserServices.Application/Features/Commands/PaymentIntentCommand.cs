using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;

namespace UserServices.Application.Features.Commands
{
    public class PaymentIntentCommand : IRequest<PaymentIntentDTO>
    {
        public int ProjectId { get; set; }
    }
}
