using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;

namespace TaskServices.Application.Features.Queries.Pages
{
    public class GetChildPageListQuery : IRequest<List<PageDTO>>
    {
        public int ParentPageId { get; set; }
        public GetChildPageListQuery(int parentPageId)
        {
            ParentPageId = parentPageId;
        }
    }
}
