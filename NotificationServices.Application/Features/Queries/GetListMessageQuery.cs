using MediatR;
using NotificationServices.Application.DTOs;
using NotificationServices.Shared.Pagination.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Features.Queries
{
    public class GetListMessageQuery : IRequest<List<MessageDTO>>
    {
        
        public int Id { get; set; }
        public GetListMessageQuery(int id)
        {
            Id = id;
        }
    }
}
