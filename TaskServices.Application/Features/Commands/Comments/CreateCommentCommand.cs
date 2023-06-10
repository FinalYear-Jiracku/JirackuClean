using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Comments
{
    public class CreateCommentCommand : IRequest<Comment>
    {
        public string? Content { get; set; }
        public int IssueId { get; set; }
        public int SubIssueId { get; set; }
        public int NoteId { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class CommentCreatedEvent : BaseEvent
    {
        public Comment Comment { get; }

        public CommentCreatedEvent(Comment comment)
        {
            Comment = comment;
        }
    }
}
