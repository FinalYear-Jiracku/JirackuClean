using NotificationServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string? GroupName { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
