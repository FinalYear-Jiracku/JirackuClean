using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.SubIssues
{
    public class DeleteSubIssueCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteSubIssueCommand(int id)
        {
            Id = id;
        }
    }
    public class SubIssueDeletedEvent : BaseEvent
    {
        public SubIssue SubIssue { get; }

        public SubIssueDeletedEvent(SubIssue subIssue)
        {
            SubIssue = subIssue;
        }
    }
}
