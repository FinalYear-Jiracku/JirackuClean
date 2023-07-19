using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Columns;
using TaskServices.Application.Features.Commands.Comments;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Comments
{
    public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentRepository.GetCommentById(command.Id);
            if (comment == null)
            {
                return default;
            }
            if (command.IssueId != 0)
            {
                var issue = await _unitOfWork.IssueRepository.GetIssueById(command.IssueId);
                comment.Issue = issue;
            }
            if (command.SubIssueId != 0)
            {
                var subIssue = await _unitOfWork.SubIssueRepository.GetSubIssueById(command.SubIssueId);
                comment.SubIssue = subIssue;
            }
            if (command.NoteId != 0)
            {
                var note = await _unitOfWork.NoteRepository.GetNoteById(command.NoteId);
                comment.Note = note;
            }
            comment.Content = command.Content;
            comment.UserId = command.UserId;
            comment.UpdatedBy = command.UpdatedBy;
            comment.UpdatedAt = DateTimeOffset.Now;
            await _unitOfWork.Repository<Comment>().UpdateAsync(comment);
            comment.AddDomainEvent(new CommentUpdatedEvent(comment));
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
