using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Issues
{
    public class DeleteIssueCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteIssueCommand(int id)
        {
            Id = id;
        }
    }
    public class IssueDeletedEvent : BaseEvent
    {
        public Issue Issue { get; }

        public IssueDeletedEvent(Issue issue)
        {
            Issue = issue;
        }
    }
}
