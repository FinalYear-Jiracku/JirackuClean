using NotificationServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Domain.Entities
{
    public class EmailLog : BaseAuditableEntity
    {
        public string? Email { get; set; }
        public string? Log { get; set; }
        public DateTimeOffset? EmailSent { get; set; }
    }
}
