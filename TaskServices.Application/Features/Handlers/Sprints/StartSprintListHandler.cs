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

namespace TaskServices.Application.Features.Handlers.Sprints
{
    public class StartSprintListHandler : IRequestHandler<StartSprintListQuery, List<SprintDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public StartSprintListHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<List<SprintDTO>> Handle(StartSprintListQuery query, CancellationToken cancellationToken)
        {
            var sprint = await _unitOfWork.SprintRepository.GetStartSprintListByProjectId(query.Id);
            var sprintDto = _mapper.Map<List<SprintDTO>>(sprint).OrderByDescending(x => x.Id).ToList();
            return sprintDto;
        }
    }
}
