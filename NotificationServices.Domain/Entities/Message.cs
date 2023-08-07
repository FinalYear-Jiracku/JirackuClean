using NotificationServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Domain.Entities
{
    public class Message : BaseEntity
    {
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
        public string? Content { get; set; }
        public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
        public User? User { get; set; }
        public Group? Group { get; set; }
    }
}
