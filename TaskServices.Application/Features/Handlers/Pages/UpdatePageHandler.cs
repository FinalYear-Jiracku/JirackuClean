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
    public class UpdatePageHandler : IRequestHandler<UpdatePageCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePageHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdatePageCommand command, CancellationToken cancellationToken)
        {
            var page = await _unitOfWork.PageRepository.GetPageById(command.Id);
            if (page == null)
            {
                return default;
            }
            page.Title = command.Title;
            page.Content = command.Content;
            page.UpdatedBy = command.UpdatedBy;
            page.UpdatedAt = DateTimeOffset.Now;
            await _unitOfWork.Repository<Page>().UpdateAsync(page);
            page.AddDomainEvent(new PageUpdatedEvent(page));
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
