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
    public class UpdateCommentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int IssueId { get; set; }
        public int SubIssueId { get; set; }
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class CommentUpdatedEvent : BaseEvent
    {
        public Comment Comment { get; }

        public CommentUpdatedEvent(Comment comment)
        {
            Comment = comment;
        }
    }
}
