using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class Sprint : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public bool? IsCompleted { get; set; } = false;
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
        public ICollection<Status>? Statuses { get; set; }
        public ICollection<Issue>? Issues { get; set; }
        public ICollection<Column>? Columns { get; set; }
        public ICollection<Page>? Pages { get; set; }
    }
}
