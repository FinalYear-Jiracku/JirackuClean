using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Pages;
using TaskServices.Application.Interfaces;

namespace TaskServices.Application.Features.Handlers.Pages
{
    public class GetChildPageListHandler : IRequestHandler<GetChildPageListQuery, List<PageDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetChildPageListHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<List<PageDTO>> Handle(GetChildPageListQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<List<PageDTO>>($"ChildPageDTO{query.ParentPageId}");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return cacheData;
            }
            var page = await _unitOfWork.PageRepository.GetChildPageListByParentPageId(query.ParentPageId);
            var pageDto = _mapper.Map<List<PageDTO>>(page);
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<PageDTO>>($"ChildPageDTO{query.ParentPageId}", pageDto, expireTime);
            return pageDto;
        }
    }
}
