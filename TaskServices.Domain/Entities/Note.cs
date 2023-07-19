using System;
using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class Note : BaseAuditableEntity
    {
        public string? Content { get; set; }
        public int? Order { get; set; }
        public int? UserId { get; set; }
        public int? ColumnId { get; set; }
        public Column? Column { get; set; }
        public User? User { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
