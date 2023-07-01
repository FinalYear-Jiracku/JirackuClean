﻿using AutoMapper;
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
    public class DeleteSprintHandler : IRequestHandler<DeleteSprintCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public DeleteSprintHandler(IUnitOfWork unitOfWork, ICacheService cacheService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteSprintCommand command, CancellationToken cancellationToken)
        {
            var sprint = await _unitOfWork.SprintRepository.GetSprintById(command.Id);
            if (sprint == null)
            {
                return default;
            }
            sprint.IsDeleted = true;
            await _unitOfWork.Repository<Sprint>().UpdateAsync(sprint);
            await _unitOfWork.Save(cancellationToken);
            var sprints = await _unitOfWork.SprintRepository.GetSprintListByProjectId(sprint.ProjectId);
            var sprintsDto = _mapper.Map<List<SprintDTO>>(sprints);
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<SprintDTO>>($"SprintDTO?projectId={sprint.ProjectId}", sprintsDto, expireTime);
            return await Task.FromResult(0);
        }
    }
}
