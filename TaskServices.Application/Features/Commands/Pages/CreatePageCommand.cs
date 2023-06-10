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
    public class CreatePageCommand : IRequest<Page>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? ParentPageId { get; set; }
        public int? UserId { get; set; }
        public int? SprintId { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class PageCreatedEvent : BaseEvent
    {
        public Page Page { get; }

        public PageCreatedEvent(Page page)
        {
            Page = page;
        }
    }
}
