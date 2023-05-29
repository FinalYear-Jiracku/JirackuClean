using System;
using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class Comment : BaseAuditableEntity
    {
        public string? Content { get; set; }
        public Note? Note { get; set; }
        public Issue? Issue { get; set; }
        public SubIssue? SubIssue { get; set; }
    }
}
