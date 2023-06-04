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
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }
        public ICollection<UserIssue>? UserIssues { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
        public UpdateIssueCommand(int id, 
                                  string? name,
                                  string? description,
                                  IssueType? type,
                                  IssuePriority? priority,
                                  int? storyPoint,
                                  int? statusId,
                                  DateTimeOffset? startDate,
                                  DateTimeOffset? dueDate,
                                  ICollection<Attachment>? attachments,
                                  ICollection<UserIssue>? userIssues,
                                  string? updatedBy, 
                                  DateTimeOffset? updatedAt)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            Priority = priority;
            StoryPoint = storyPoint;
            StatusId = statusId;
            StartDate = startDate;
            DueDate = dueDate;
            Attachments = attachments;
            UserIssues = userIssues;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
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
