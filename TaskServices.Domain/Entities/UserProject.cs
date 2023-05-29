namespace TaskServices.Domain.Entities
{
    public class UserProject
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;
        public Project? Project { get; set; }

    }
}
