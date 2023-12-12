using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? Role { get; set; }
        public ICollection<Issue>? Issues { get; set; }
        public ICollection<UserProject>? UserProjects { get; set; }
        public ICollection<SubIssue>? SubIssues { get; set; }
        public ICollection<Page>? Pages { get; set; }
        public ICollection<Note>? Notes { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
