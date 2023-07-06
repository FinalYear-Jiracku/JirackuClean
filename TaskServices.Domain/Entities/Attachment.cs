using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class Attachment : BaseAuditableEntity
    {
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public byte[]? FileData { get; set; }
        public Issue? Issue { get; set; }
        public SubIssue? SubIssue { get; set; }
    }
}
