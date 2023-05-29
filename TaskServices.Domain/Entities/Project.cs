using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class Project : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public string? Lead { get; set; }
        public bool? IsUpgraded { get; set; } = false;
        public ICollection<Sprint>? Sprints { get; set; }
        public ICollection<UserProject>? UserProjects { get; set; }
    }
}
