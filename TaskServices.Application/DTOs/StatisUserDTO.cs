using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.DTOs
{
    public class StatisUserDTO
    {
        public string? Email { get; set; }
        public List<StatusCountDTO>? StatusCounts { get; set; }
    }
}
