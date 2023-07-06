namespace TaskServices.Application.DTOs
{
    public class DataColumnDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public ICollection<NoteDTO>? Notes { get; set; }
    }
}
