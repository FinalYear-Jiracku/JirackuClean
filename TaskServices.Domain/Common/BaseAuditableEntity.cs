using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common.Interfaces;

namespace TaskServices.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public bool? IsDeleted { get; set; } = false;
        public string? CreatedBy { get; set; }
        public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
