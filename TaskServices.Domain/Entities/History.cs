using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class History : BaseAuditableEntity
    {
        public int? IssueId { get; set; }
        public string? EventType { get; set; }
        public string? EventTime { get; set; }
        public int? UserId { get; set; }
        public string? Changes { get; set; }
    }
}
