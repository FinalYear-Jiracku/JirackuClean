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
    public class UpdateColumnHandler : IRequestHandler<UpdateColumnCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateColumnHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateColumnCommand command, CancellationToken cancellationToken)
        {
            var column = await _unitOfWork.ColumnRepository.GetColumnById(command.Id);
            if (column == null)
            {
                return default;
            }
            column.Name = command.Name;
            column.Color = command.Color;
            column.UpdatedBy = command.UpdatedBy;
            column.UpdatedAt = DateTimeOffset.Now;
            await _unitOfWork.Repository<Column>().UpdateAsync(column);
            column.AddDomainEvent(new ColumnUpdatedEvent(column));
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
