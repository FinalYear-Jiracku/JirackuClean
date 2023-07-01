using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities.Enums;
using TaskServices.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace TaskServices.Application.Features.Commands.SubIssues
{
    public class UpdateSubIssueCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IssueType? Type { get; set; }
        public IssuePriority? Priority { get; set; }
        public int? StoryPoint { get; set; }
        public int? StatusId { get; set; }
        public int? IssueId { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public List<IFormFile>? Files { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }
        public ICollection<UserIssue>? UserIssues { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class SubIssueUpdatedEvent : BaseEvent
    {
        public SubIssue SubIssue { get; }

        public SubIssueUpdatedEvent(SubIssue subIssue)
        {
            SubIssue = subIssue;
        }
    }
}
