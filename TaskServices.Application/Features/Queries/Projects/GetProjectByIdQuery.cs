using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Projects
{
    public class GetProjectByIdQuery : IRequest<ProjectDTO>
    {
        public int Id { get; set; }
        public GetProjectByIdQuery(int id)
        {
            Id = id;
        }
    }
}
