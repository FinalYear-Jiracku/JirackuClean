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
    public class GetListNotificationQuery : IRequest<List<NotificationDTO>>
    {
        public int Id { get; set; }
        public GetListNotificationQuery(int id)
        {
            Id = id;
        }
    }
}
