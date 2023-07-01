using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Columns
{
    public class GetColumnByIdQuery : IRequest<ColumnDTO>
    {
        public int Id { get; set; }
        public GetColumnByIdQuery(int id)
        {
            Id = id;
        }
    }
}
