using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Entities.Enums;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.DTOs
{
    public class DataIssueDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IssueType? Type { get; set; }
        public IssuePriority? Priority { get; set; }
        public int? StoryPoint { get; set; }
        public string? SprintName { get; set; }
        public string? Status { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }
        public ICollection<SubIssue>? SubIssues { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<UserIssue>? UserIssues { get; set; }
        
    }
}
