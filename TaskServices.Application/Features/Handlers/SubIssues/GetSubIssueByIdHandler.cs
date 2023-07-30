using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.SubIssues;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Application.Features.Handlers.SubIssues
{
    public class GetSubIssueByIdHandler : IRequestHandler<GetSubIssueByIdQuery, DataSubIssueDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetSubIssueByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<DataSubIssueDTO> Handle(GetSubIssueByIdQuery query, CancellationToken cancellationToken)
        {
            //var cacheData = _cacheService.GetData<DataSubIssueDTO>($"DataSubIssueDTO{query.Id}");
            //if (cacheData != null)
            //{
            //    return cacheData;
            //}
            var subIssue = await _unitOfWork.SubIssueRepository.GetSubIssueById(query.Id);
            var subIssueDto = _mapper.Map<DataSubIssueDTO>(subIssue);
            if (subIssueDto == null)
            {
                return null;
            }
            //var expireTime = DateTimeOffset.Now.AddSeconds(30);
            //_cacheService.SetData<DataSubIssueDTO>($"DataSubIssueDTO{subIssueDto.Id}", subIssueDto, expireTime);
            return subIssueDto;
        }
    }
}
