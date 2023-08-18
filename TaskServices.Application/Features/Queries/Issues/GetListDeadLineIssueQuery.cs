using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Issues
{
    public class GetListDeadLineIssueQuery : IRequest<List<DeadlineIssuesDTO>>
    {
        public int Id { get; set; }
        public GetListDeadLineIssueQuery(int id)
        {
            Id = id;
        }
    }
}
