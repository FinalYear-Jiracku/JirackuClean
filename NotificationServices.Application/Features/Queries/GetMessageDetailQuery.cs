using MediatR;
using NotificationServices.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Features.Queries
{
    public class GetMessageDetailQuery : IRequest<MessageDTO>
    {
        public int Id { get; set; }
        public GetMessageDetailQuery(int id)
        {
            Id = id;
        }
    }
}
