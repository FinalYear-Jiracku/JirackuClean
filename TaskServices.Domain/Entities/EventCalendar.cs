using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class EventCalendar : BaseAuditableEntity
    {
        public string? Title { get; set; }
        public string? Link { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
