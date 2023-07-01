using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Domain.Entities
{
    public class UserIssue
    {
        public int IssueId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset? JoinDate { get; set; } = DateTimeOffset.Now;
        public Issue? Issue { get; set; }
    }
}
