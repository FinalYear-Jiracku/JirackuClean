using AutoMapper;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Application.Interfaces;

namespace TaskServices.Infrastructure.Services
{
    public class CheckEventJob : IJob
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICheckEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        public CheckEventJob(IMapper mapper,
                             ICheckEventPublisher eventPublisher,
                             IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _eventRepository = eventRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var date = DateTimeOffset.UtcNow;
            var checkEvent = await _eventRepository.CheckEventCalendar();
            var checkEventDTO = _mapper.Map<List<EventCalendarDTO>>(checkEvent);
            _eventPublisher.SendMessage(checkEventDTO);
        }
    }
}
