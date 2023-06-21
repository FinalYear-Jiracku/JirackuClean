using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Columns;
using TaskServices.Application.Features.Queries.Pages;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Application.Features.Handlers.Pages
{
    public class GetPageListHandler : IRequestHandler<GetPageListQuery, List<PageDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetPageListHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<List<PageDTO>> Handle(GetPageListQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<List<PageDTO>>($"PageDTO{query.Id}");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return cacheData;
            }
            var page = await _unitOfWork.PageRepository.GetPageListBySprintId(query.Id);
            var pageDto = _mapper.Map<List<PageDTO>>(page);
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<PageDTO>>($"PageDTO{query.Id}", pageDto, expireTime);
            return pageDto;
        }
    }
}
