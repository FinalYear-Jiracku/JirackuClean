using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Pages
{
    public class GetPageListQuery : IRequest<List<PageDTO>>
    {
        public int Id { get; set; }
        public GetPageListQuery(int id)
        {
            Id = id;
        }
    }
}
