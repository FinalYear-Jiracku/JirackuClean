using System;
using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class Column : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public string? Color { get; set; }
        public int? SprintId { get; set; }
        public Sprint? Sprint { get; set; }
        public ICollection<Note>? Notes { get; set; }
    }
}
