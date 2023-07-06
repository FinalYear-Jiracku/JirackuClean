using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Domain.Entities
{
    public class UserSubIssue
    {
        public int SubIssueId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset? JoinDate { get; set; } = DateTimeOffset.Now;
        public SubIssue? SubIssue { get; set; }
        public User? User { get; set; }
    }
}
