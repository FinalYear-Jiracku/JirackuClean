using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Features.Commands
{
    public class UpdateMessageCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
