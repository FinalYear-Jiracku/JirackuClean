using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Queries.Events
{
    public class GetListEventQuery : IRequest<List<EventDTO>>
    {
        public int Id { get; set; }
        public GetListEventQuery(int id)
        {
            Id = id;
        }
    }
}
