using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.Features.Queries.Issues
{
    public class CountIssueCompletedQuery : IRequest<int>
    {
        public int Id { get; set; }
        public CountIssueCompletedQuery(int id)
        {
            Id = id;
        }
    }
}
