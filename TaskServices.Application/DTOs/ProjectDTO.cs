using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsUpgraded { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
