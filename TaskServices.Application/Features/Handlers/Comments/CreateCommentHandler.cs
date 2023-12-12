using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Comments;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Comments
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, Comment>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Comment> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
        {
            var newComment = new Comment()
            {
                Content = command.Content,
                UserId = command.UserId,
                CreatedBy = command.CreatedBy,
            };
            if(command.IssueId != 0)
            {
                var issue = await _unitOfWork.IssueRepository.GetIssueById(command.IssueId);
                newComment.Issue = issue;
            }
            if (command.SubIssueId != 0)
            {
                var subIssue = await _unitOfWork.SubIssueRepository.GetSubIssueById(command.SubIssueId);
                newComment.SubIssue = subIssue;
            }
            if (command.NoteId != 0)
            {
                var note = await _unitOfWork.NoteRepository.GetNoteById(command.NoteId);
                newComment.Note = note;
            }
            await _unitOfWork.Repository<Comment>().AddAsync(newComment);
            //newComment.AddDomainEvent(new CommentCreatedEvent(newComment));
            await _unitOfWork.Save(cancellationToken);
            return newComment;
        }
    }
}
