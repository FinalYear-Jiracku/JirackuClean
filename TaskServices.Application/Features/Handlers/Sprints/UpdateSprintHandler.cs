using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Sprints;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Sprints
{
    public class UpdateSprintHandler : IRequestHandler<UpdateSprintCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public UpdateSprintHandler(IUnitOfWork unitOfWork, ICacheService cacheService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateSprintCommand command, CancellationToken cancellationToken)
        {
            var sprint = await _unitOfWork.SprintRepository.GetSprintById(command.Id);
            if (sprint == null)
            {
                return default;
            }
            sprint.Name = command.Name;
            sprint.ProjectId = command.ProjectId;
            sprint.StartDate = command.StartDate;
            sprint.EndDate = command.EndDate;
            sprint.UpdatedBy = command.UpdatedBy;
            sprint.UpdatedAt = DateTimeOffset.Now;
            await _unitOfWork.Repository<Sprint>().UpdateAsync(sprint);
            sprint.AddDomainEvent(new SprintUpdatedEvent(sprint));
            await _unitOfWork.Save(cancellationToken);
            var sprints = await _unitOfWork.SprintRepository.GetSprintListByProjectId(command.ProjectId);
            var sprintsDto = _mapper.Map<List<SprintDTO>>(sprints).OrderByDescending(x => x.Id).Take(8).ToList();
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<SprintDTO>>($"SprintDTO?projectId={command.ProjectId}&pageNumber=1&search=", sprintsDto, expireTime);
            return await Task.FromResult(0);
        }
    }
}
