using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.DTOs
{
    public class AttachmentDTO
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; } // Loại tệp (vd: "PDF", "XSLX", "PNG", "MP4", "DOC")
        public byte[]? FileData { get; set; } // Dữ liệu tệp
    }
}
