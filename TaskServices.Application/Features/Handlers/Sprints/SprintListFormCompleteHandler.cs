﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Application.Features.Handlers.Sprints
{
    public class SprintListFormCompleteHandler : IRequestHandler<SprintListForCompleteQuery, List<SprintDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public SprintListFormCompleteHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<List<SprintDTO>> Handle(SprintListForCompleteQuery query, CancellationToken cancellationToken)
        {
            //var cacheData = _cacheService.GetData<List<SprintDTO>>($"SprintDTO/dropdown?projectId={query.Id}");
            //if (cacheData != null && cacheData.Count() > 0)
            //{
            //    return cacheData;
            //}
            var sprint = await _unitOfWork.SprintRepository.GetSprintListForComplete(query.Id,query.SprintId);
            var sprintDto = _mapper.Map<List<SprintDTO>>(sprint).OrderByDescending(x => x.Id).ToList();
            //var expireTime = DateTimeOffset.Now.AddSeconds(30);
            //_cacheService.SetData<List<SprintDTO>>($"SprintDTO/dropdown?projectId={query.Id}", issueDto, expireTime);
            return sprintDto;
        }
    }
}
