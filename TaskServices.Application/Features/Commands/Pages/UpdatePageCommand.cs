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
    public class UpdatePageCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class PageUpdatedEvent : BaseEvent
    {
        public Page Page { get; }

        public PageUpdatedEvent(Page page)
        {
            Page = page;
        }
    }
}
