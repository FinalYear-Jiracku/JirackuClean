using MediatR;
using Microsoft.AspNetCore.Http;
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
    public class UpdateIssueCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IssueType? Type { get; set; }
        public IssuePriority? Priority { get; set; }
        public int? StoryPoint { get; set; }
        public int? StatusId { get; set; }
        public int? SprintId { get; set; }
        public int? UserId { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public List<IFormFile>? Files { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class IssueUpdatedEvent : BaseEvent
    {
        public Issue Issue { get; }

        public IssueUpdatedEvent(Issue issue)
        {
            Issue = issue;
        }
    }
}
