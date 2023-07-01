using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;
using TaskServices.Domain.Entities.Enums;

namespace TaskServices.Application.Features.Commands.Issues
{
    public class CreateIssueCommand : IRequest<Issue>
    {
        public string? Name { get; set; }
        public IssueType? Type { get; set; }
        public IssuePriority? Priority { get; set; }
        public int? StoryPoint { get; set; }
        public int? StatusId { get; set; }
        public int? SprintId { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public ICollection<UserIssue>? UserIssues { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class IssueCreatedEvent : BaseEvent
    {
        public Issue Issue { get; }

        public IssueCreatedEvent(Issue issue)
        {
            Issue = issue;
        }
    }
}
