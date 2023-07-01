using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Queries.Statuses
{
    public class DropdownStatusListQuery : IRequest<List<StatusDTO>>
    {
        public int Id { get; set; }
        public DropdownStatusListQuery(int id)
        {
            Id = id;
        }
    }
}
