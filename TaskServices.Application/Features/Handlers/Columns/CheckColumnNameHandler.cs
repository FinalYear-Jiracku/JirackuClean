using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Columns;
using TaskServices.Application.Interfaces;

namespace TaskServices.Application.Features.Handlers.Columns
{
    public class CheckColumnNameHandler : IRequestHandler<CheckColumnNameQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CheckColumnNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CheckColumnNameQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ColumnRepository.CheckColumnName(query);
        }
    }
}
