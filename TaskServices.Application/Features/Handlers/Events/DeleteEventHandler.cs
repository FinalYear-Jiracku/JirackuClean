using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Events;
using TaskServices.Application.Features.Commands.Sprints;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Events
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public DeleteEventHandler(IUnitOfWork unitOfWork, ICacheService cacheService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteEventCommand command, CancellationToken cancellationToken)
        {
            var findEvent = await _unitOfWork.EventRepository.GetEventById(command.Id);
            if (findEvent == null)
            {
                return default;
            }
            findEvent.IsDeleted = true;
            await _unitOfWork.Repository<EventCalendar>().UpdateAsync(findEvent);
            await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(0);
        }
    }
}
