using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Statuses;
using TaskServices.Application.Interfaces;

namespace TaskServices.Application.Features.Handlers.Statuses
{
    public class CheckStatusNameHandler : IRequestHandler<CheckStatusNameQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CheckStatusNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CheckStatusNameQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.StatusRepository.CheckStatusName(query);
        }
    }
}
