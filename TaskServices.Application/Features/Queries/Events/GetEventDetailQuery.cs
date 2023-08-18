using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Events
{
    public class GetEventDetailQuery : IRequest<EventDTO>
    {
        public int Id { get; set; }
        public GetEventDetailQuery(int id)
        {
            Id = id;
        }
    }
}
