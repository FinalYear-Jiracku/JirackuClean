using AutoMapper;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Columns;
using TaskServices.Application.Features.Commands.Sprints;
using TaskServices.Application.Features.Commands.Statuses;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Sprints
{
    public class CreateSprintHandler : IRequestHandler<CreateSprintCommand, Sprint>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public CreateSprintHandler(IUnitOfWork unitOfWork, ICacheService cacheService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<Sprint> Handle(CreateSprintCommand command, CancellationToken cancellationToken)
        {
            var newSprint = new Sprint()
            {
                Name = command.Name,
                ProjectId = command.ProjectId,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                CreatedBy = command.CreatedBy,
            };
            newSprint.Statuses = new List<Status>();
            var status1 = new Status()
            {
                Name = "ToDo",
                Color = "#dcfce7",
                CreatedBy = newSprint.CreatedBy,
            };
            status1.AddDomainEvent(new StatusCreatedEvent(status1));
            newSprint.Statuses.Add(status1);
            
            var status2 = new Status()
            {
                Name = "InProgress",
                Color = "#fee2e2",
                CreatedBy = newSprint.CreatedBy,
            };
            status2.AddDomainEvent(new StatusCreatedEvent(status2));
            newSprint.Statuses.Add(status2);
            
            var status3 = new Status()
            {
                Name = "Done",
                Color = "#e0f2fe",
                CreatedBy = newSprint.CreatedBy,
            };
            status3.AddDomainEvent(new StatusCreatedEvent(status3));
            newSprint.Statuses.Add(status3);

            newSprint.Columns = new List<Column>();
            var column1 = new Column
            {
                Name = "Problem",
                Color = "#dcfce7",
                CreatedBy = newSprint.CreatedBy,
            };
            column1.AddDomainEvent(new ColumnCreatedEvent(column1));
            newSprint.Columns.Add(column1);

            var column2 = new Column
            {
                Name = "Try",
                Color = "#dcfce7",
                CreatedBy = newSprint.CreatedBy,
            };
            column2.AddDomainEvent(new ColumnCreatedEvent(column2));
            newSprint.Columns.Add(column2);

            var column3 = new Column
            {
                Name = "Keep",
                Color = "#dcfce7",
                CreatedBy = newSprint.CreatedBy,
            };
            column2.AddDomainEvent(new ColumnCreatedEvent(column2));
            newSprint.Columns.Add(column3);
            await _unitOfWork.Repository<Sprint>().AddAsync(newSprint);
            newSprint.AddDomainEvent(new SprintCreatedEvent(newSprint));
            await _unitOfWork.Save(cancellationToken);
            var sprints = await _unitOfWork.SprintRepository.GetSprintListByProjectId(command.ProjectId);
            var sprintsDto = _mapper.Map<List<SprintDTO>>(sprints).OrderByDescending(x => x.Id).Take(8).ToList();
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<SprintDTO>>($"SprintDTO?projectId={command.ProjectId}&pageNumber=1&search=", sprintsDto, expireTime);
            return newSprint;
        }
    } 
}
