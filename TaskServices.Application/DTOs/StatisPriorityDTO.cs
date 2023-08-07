using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.DTOs
{
    public class StatisPriorityDTO
    {
        public double Urgent { get; set; }
        public double High { get; set; }
        public double Normal { get; set; }
        public double Low { get; set; }
    }
}
