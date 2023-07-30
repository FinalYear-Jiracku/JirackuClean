using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.DTOs
{
    public class NotificationDTO
    {
        public int ProjectId { get; set; }
        public string? Content { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
