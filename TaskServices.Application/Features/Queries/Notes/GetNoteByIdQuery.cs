using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Notes
{
    public class GetNoteByIdQuery : IRequest<NoteDTO>
    {
        public int Id { get; set; }
        public GetNoteByIdQuery(int id)
        {
            Id = id;
        }
    }
}
