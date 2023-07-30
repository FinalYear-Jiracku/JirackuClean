namespace TaskServices.Application.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public UserDTO? User { get; set; }
    }
}
