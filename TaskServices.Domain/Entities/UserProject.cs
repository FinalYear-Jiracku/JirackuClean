using TaskServices.Domain.Common;

namespace TaskServices.Domain.Entities
{
    public class UserProject
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset? JoinDate { get; set; } = DateTimeOffset.Now;
        public Project? Project { get; set; }
        public User? User { get; set; }
    }
}
