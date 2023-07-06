using TaskServices.Domain.Entities;
using TaskServices.Domain.Entities.Enums;

namespace TaskServices.Application.DTOs
{
    public class IssueDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IssueType? Type { get; set; }
        public IssuePriority? Priority { get; set; }
        public int? StoryPoint { get; set; }
        public StatusDTO? Status { get; set; }
        public int? SubIssues { get; set; }
        public SprintDTO? Sprint { get; set; }
        public ICollection<UserIssue>? UserIssues { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
    }
}
