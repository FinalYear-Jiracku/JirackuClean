using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Columns;
using TaskServices.Application.Features.Commands.Pages;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Pages
{
    public class DeletePageHandler : IRequestHandler<DeletePageCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeletePageHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeletePageCommand command, CancellationToken cancellationToken)
        {
            var page = await _unitOfWork.PageRepository.GetPageById(command.Id);
            if (page == null)
            {
                return default;
            }
            page.IsDeleted = true;
            await _unitOfWork.Repository<Page>().UpdateAsync(page);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
