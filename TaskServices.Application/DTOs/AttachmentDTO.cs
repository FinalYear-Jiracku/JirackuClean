namespace TaskServices.Application.DTOs
{
    public class AttachmentDTO
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public byte[]? FileData { get; set; }
    }
}
