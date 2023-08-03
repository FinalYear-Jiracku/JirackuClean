using NotificationServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? UserName { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
