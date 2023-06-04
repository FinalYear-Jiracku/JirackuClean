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

namespace TaskServices.Application.Features.Handlers.Sprints
{
    public class GetSprintByIdHandler : IRequestHandler<GetSprintByIdQuery, SprintDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetSprintByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<SprintDTO> Handle(GetSprintByIdQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<SprintDTO>($"sprints{query.Id}");
            if (cacheData != null)
            {
                return cacheData;
            }
            var sprint = await _unitOfWork.SprintRepository.GetSprintById(query.Id);
            var sprintDto = _mapper.Map<SprintDTO>(sprint);
            if (sprintDto == null)
            {
                return null;
            }
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<SprintDTO>($"sprints{sprintDto.Id}", sprintDto, expireTime);
            return sprintDto;
        }
    }
}
