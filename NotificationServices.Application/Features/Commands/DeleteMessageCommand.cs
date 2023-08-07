using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Features.Commands
{
    public class DeleteMessageCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteMessageCommand(int id)
        {
            Id = id;
        }
    }
}
