using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Columns;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Columns
{
    public class DeleteColumnHandler : IRequestHandler<DeleteColumnCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteColumnHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteColumnCommand command, CancellationToken cancellationToken)
        {
            var column = await _unitOfWork.ColumnRepository.GetColumnById(command.Id);
            if (column == null)
            {
                return default;
            }
            column.IsDeleted = true;
            await _unitOfWork.Repository<Column>().UpdateAsync(column);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
