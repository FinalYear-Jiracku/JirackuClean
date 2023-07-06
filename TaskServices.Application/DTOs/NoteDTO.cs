namespace TaskServices.Application.DTOs
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? CreatedBy { get; set; }
        public ICollection<CommentDTO>? Comments { get; set; }
    }
}
