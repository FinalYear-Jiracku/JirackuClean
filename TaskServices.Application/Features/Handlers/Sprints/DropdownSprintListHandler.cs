using AutoMapper;
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
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Sprints
{
    public class DropdownSprintListHandler : IRequestHandler<DropdownSprintListQuery, List<SprintDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public DropdownSprintListHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<List<SprintDTO>> Handle(DropdownSprintListQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<List<SprintDTO>>($"SprintDTO?projectId={query.Id}");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return cacheData;
            }
            var status = await _unitOfWork.SprintRepository.GetSprintListByProjectId(query.Id);
            var issueDto = _mapper.Map<List<SprintDTO>>(status).OrderByDescending(x => x.Id).ToList();
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<SprintDTO>>($"SprintDTO?projectId={query.Id}", issueDto, expireTime);
            return issueDto;
        }
    }
}
