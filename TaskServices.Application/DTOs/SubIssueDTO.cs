using TaskServices.Domain.Entities.Enums;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.DTOs
{
    public class SubIssueDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IssueType? Type { get; set; }
        public IssuePriority? Priority { get; set; }
        public int? StoryPoint { get; set; }
        public string? Status { get; set; }
        public ICollection<UserSubIssue>? UserSubIssues { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
    }
}
