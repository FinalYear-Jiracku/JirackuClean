using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Pages
{
    public class DeletePageCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeletePageCommand(int id)
        {
            Id = id;
        }
    }
    public class PageDeletedEvent : BaseEvent
    {
        public Page Page { get; }

        public PageDeletedEvent(Page page)
        {
            Page = page;
        }
    }
}
