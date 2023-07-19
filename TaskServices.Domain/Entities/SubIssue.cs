using TaskServices.Domain.Common;
using TaskServices.Domain.Entities.Enums;

namespace TaskServices.Domain.Entities
{
    public class SubIssue : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IssueType? Type { get; set; }
        public IssuePriority? Priority { get; set; }
        public int? StoryPoint { get; set; }
        public int? Order { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public int? StatusId { get; set; }
        public int? IssueId { get; set; }
        public Status? Status { get; set; }
        public Issue? Issue { get; set; }
        public User? User { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
