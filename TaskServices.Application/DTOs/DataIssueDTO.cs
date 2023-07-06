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
        public SprintDTO? Sprint { get; set; }
        public StatusDTO? Status { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public ICollection<AttachmentDTO>? Attachments { get; set; }
        public ICollection<SubIssueDTO>? SubIssues { get; set; }
        public ICollection<CommentDTO>? Comments { get; set; }
        public ICollection<UserIssue>? UserIssues { get; set; }
        
    }
}
