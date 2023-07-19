using TaskServices.Domain.Entities.Enums;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.DTOs
{
    public class DataSubIssueDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IssueType? Type { get; set; }
        public IssuePriority? Priority { get; set; }
        public int? StoryPoint { get; set; }
        public StatusDTO? Status { get; set; }
        public UserDTO? User { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public ICollection<AttachmentDTO>? Attachments { get; set; }
        public ICollection<CommentDTO>? Comments { get; set; }
        
    }
}
