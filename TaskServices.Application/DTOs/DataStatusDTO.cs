namespace TaskServices.Application.DTOs
{
    public class DataStatusDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public ICollection<IssueDTO>? Issues { get; set; }
    }
}
