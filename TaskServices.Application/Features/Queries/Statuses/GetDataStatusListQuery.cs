using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Queries.Statuses
{
    public class GetDataStatusListQuery : IRequest<List<DataStatusDTO>>
    {
        public int Id { get; set; }
        public GetDataStatusListQuery(int id)
        {
            Id = id;
        }
    }
}
