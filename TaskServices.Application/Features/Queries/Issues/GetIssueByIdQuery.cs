using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Issues
{
    public class GetIssueByIdQuery : IRequest<DataIssueDTO>
    {
        public int Id { get; set; }
        public GetIssueByIdQuery(int id)
        {
            Id = id;
        }
    }
}
