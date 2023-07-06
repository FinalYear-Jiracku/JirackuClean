namespace TaskServices.Application.DTOs
{
    public class PageDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? ParentPageId { get; set; }
        public int? UserId { get; set; }
    }
}
