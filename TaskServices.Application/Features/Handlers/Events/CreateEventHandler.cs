using AutoMapper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Columns;
using TaskServices.Application.Features.Commands.Events;
using TaskServices.Application.Features.Commands.Sprints;
using TaskServices.Application.Features.Commands.Statuses;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Events
{
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, EventCalendar>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public static string ClientId = "Pl2SM1rXQDm1uwtY99UT2g";
        public static string ClientSecret = "UpjsjVwYZ4VudI0vAOWDegFNVEBeQ5Nl";
        public static string RedirectUri = "http://localhost:4206/api/events";
        public CreateEventHandler(IUnitOfWork unitOfWork, ICacheService cacheService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _mapper = mapper;

            
        }
        public async Task<EventCalendar> Handle(CreateEventCommand command, CancellationToken cancellationToken)
        {
            var newEvent = new EventCalendar()
            {
                Title = command.Title,
                ProjectId = command.ProjectId,
                StartTime = command.StartTime,
                EndTime = command.EndTime,
                CreatedBy = command.CreatedBy,
            };

            await _unitOfWork.Repository<EventCalendar>().AddAsync(newEvent);
            newEvent.AddDomainEvent(new EventCreatedEvent(newEvent));
            await _unitOfWork.Save(cancellationToken);
            return newEvent;
        }
    }
}
