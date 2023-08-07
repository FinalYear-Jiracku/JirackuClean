using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public UserDTO? User { get; set; }
    }
}   
