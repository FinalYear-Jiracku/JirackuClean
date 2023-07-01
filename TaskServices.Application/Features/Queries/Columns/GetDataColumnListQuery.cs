using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Columns
{
    public class GetDataColumnListQuery : IRequest<List<DataColumnDTO>>
    {
        public int Id { get; set; }
        public GetDataColumnListQuery(int id)
        {
            Id = id;
        }
    }
}
