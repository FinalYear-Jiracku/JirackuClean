using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class Attachment : BaseAuditableEntity
    {
        public string? FileName { get; set; }
        public string? FileType { get; set; } // Loại tệp (vd: "PDF", "XSLX", "PNG", "MP4", "DOC")
        public byte[]? FileData { get; set; } // Dữ liệu tệp
        public Issue? Issue { get; set; }
        public SubIssue? SubIssue { get; set; }
    }
}
