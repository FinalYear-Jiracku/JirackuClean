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

namespace TaskServices.Application.Features.Handlers.Pages
{
    public class GetPageByIdHandler : IRequestHandler<GetPageByIdQuery, PageDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetPageByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<PageDTO> Handle(GetPageByIdQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<PageDTO>($"PageDTO{query.Id}");
            if (cacheData != null)
            {
                return cacheData;
            }
            var page = await _unitOfWork.PageRepository.GetPageById(query.Id);
            var pageDto = _mapper.Map<PageDTO>(page);
            if (pageDto == null)
            {
                return null;
            }
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<PageDTO>($"PageDTO{pageDto.Id}", pageDto, expireTime);
            return pageDto;
        }
    }
}
