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
    public class DeleteCommentHandler : IRequestHandler<DeleleCommentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleleCommentCommand command, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentRepository.GetCommentById(command.Id);
            if (comment == null)
            {
                return default;
            }
            comment.IsDeleted = true;
            await _unitOfWork.Repository<Comment>().UpdateAsync(comment);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
