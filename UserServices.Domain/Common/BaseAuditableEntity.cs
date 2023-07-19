using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Domain.Common.Interfaces;

namespace UserServices.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public bool? IsDeleted { get; set; } = false;
        public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
