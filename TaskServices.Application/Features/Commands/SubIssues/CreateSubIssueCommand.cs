using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities.Enums;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.SubIssues
{
    public class CreateSubIssueCommand : IRequest<SubIssue>
    {
        public string? Name { get; set; }
        public IssueType? Type { get; set; }
        public IssuePriority? Priority { get; set; }
        public int? StoryPoint { get; set; }
        public int? StatusId { get; set; }
        public int? IssueId { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class SubIssueCreatedEvent : BaseEvent
    {
        public SubIssue SubIssue { get; }

        public SubIssueCreatedEvent(SubIssue subIssue)
        {
            SubIssue = subIssue;
        }
    }
}
