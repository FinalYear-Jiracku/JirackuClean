using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.SubIssues
{
    public class GetSubIssueByIdQuery : IRequest<DataSubIssueDTO>
    {
        public int Id { get; set; }
        public GetSubIssueByIdQuery(int id)
        {
            Id = id;
        }
    }
}
