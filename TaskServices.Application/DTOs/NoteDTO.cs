using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Entities;

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
