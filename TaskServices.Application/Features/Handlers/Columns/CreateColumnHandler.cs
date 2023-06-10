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
    public class CreateColumnHandler : IRequestHandler<CreateColumnCommand, Column>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateColumnHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Column> Handle(CreateColumnCommand command, CancellationToken cancellationToken)
        {
            var newColumn = new Column()
            {
                Name = command.Name,
                Color = command.Color,
                SprintId = command.SprintId,
                CreatedBy = command.CreatedBy,
            };
            await _unitOfWork.Repository<Column>().AddAsync(newColumn);
            newColumn.AddDomainEvent(new ColumnCreatedEvent(newColumn));
            await _unitOfWork.Save(cancellationToken);
            return newColumn;
        }
    }
}
