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
    public class DeleleCommentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleleCommentCommand(int id)
        {
            Id = id;
        }
    }
    public class CommentDeletedEvent : BaseEvent
    {
        public Comment Comment { get; }

        public CommentDeletedEvent(Comment comment)
        {
            Comment = comment;
        }
    }
}
