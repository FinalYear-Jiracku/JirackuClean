using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class User :BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public ICollection<UserSubIssue>? UserSubIssues { get; set; }
        public ICollection<UserProject>? UserProjects { get; set; }
        public ICollection<UserIssue>? UserIssues { get; set; }
        public ICollection<Page>? Pages { get; set; }
        public ICollection<Note>? Notes { get; set; }
    }
}
