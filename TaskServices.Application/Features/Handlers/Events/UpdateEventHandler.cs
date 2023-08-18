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
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public UpdateEventHandler(IUnitOfWork unitOfWork, ICacheService cacheService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateEventCommand command, CancellationToken cancellationToken)
        {
            var findEvent = await _unitOfWork.EventRepository.GetEventById(command.Id);
            if (findEvent == null)
            {
                return default;
            }
            findEvent.Title = command.Title;
            findEvent.ProjectId = command.ProjectId;
            findEvent.StartTime = command.StartTime;
            findEvent.EndTime = command.EndTime;
            findEvent.UpdatedBy = command.UpdatedBy;
            findEvent.UpdatedAt = DateTimeOffset.Now;
            await _unitOfWork.Repository<EventCalendar>().UpdateAsync(findEvent);
            findEvent.AddDomainEvent(new EventUpdatedEvent(findEvent));
            await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(0);
        }
    }
}
