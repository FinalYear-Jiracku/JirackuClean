﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Issues;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class GetIssueByIdHandler : IRequestHandler<GetIssueByIdQuery, DataIssueDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetIssueByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<DataIssueDTO> Handle(GetIssueByIdQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<DataIssueDTO>($"DataIssueDTO{query.Id}");
            if (cacheData != null)
            {
                return cacheData;
            }
            var issue = await _unitOfWork.IssueRepository.GetIssueById(query.Id);
            var issueDto = _mapper.Map<DataIssueDTO>(issue);

            if (issueDto == null)
            {
                return null;
            }
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<DataIssueDTO>($"DataIssueDTO{issueDto.Id}", issueDto, expireTime);
            return issueDto;
        }
    }
}
