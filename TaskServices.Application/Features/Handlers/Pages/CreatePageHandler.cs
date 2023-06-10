using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Pages;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Pages
{
    public class CreatePageHandler : IRequestHandler<CreatePageCommand, Page>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePageHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Page> Handle(CreatePageCommand command, CancellationToken cancellationToken)
        {
            // For the first time, pass SprintId, then ParentPageId
            var newPage = new Page()
            {
                Title = command.Title,
                Content = command.Content,
                SprintId = command.SprintId,
                CreatedBy = command.CreatedBy,
            };
            if(command.ParentPageId != null)
            {
                newPage.ParentPageId = command.ParentPageId;
            }
            await _unitOfWork.Repository<Page>().AddAsync(newPage);
            newPage.AddDomainEvent(new PageCreatedEvent(newPage));
            await _unitOfWork.Save(cancellationToken);
            return newPage;
        }
    }
}
