using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Events;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Application.Features.Handlers.Events
{
    public class GetDetailEventHandler : IRequestHandler<GetEventDetailQuery, EventDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetDetailEventHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<EventDTO> Handle(GetEventDetailQuery query, CancellationToken cancellationToken)
        {
            var findEvent = await _unitOfWork.EventRepository.GetEventById(query.Id);
            var eventDto = _mapper.Map<EventDTO>(findEvent);
            if (eventDto == null)
            {
                return null;
            }
            return eventDto;
        }
    }
}
