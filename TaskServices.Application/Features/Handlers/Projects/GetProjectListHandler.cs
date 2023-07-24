using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Common.Interfaces;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Handlers.Projects
{
    public class GetProjectListHandler : IRequestHandler<GetProjectListQuery, (List<ProjectDTO>, PaginationFilter, int)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IUserEventSubscriber _userEventSubscriber;
        

        public GetProjectListHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService, IUserEventSubscriber userEventSubscriber)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
            _userEventSubscriber = userEventSubscriber;
        }
        public async Task<(List<ProjectDTO>, PaginationFilter, int)> Handle(GetProjectListQuery query, CancellationToken cancellationToken)
        {
            await _userEventSubscriber.ReceiveMessage();
            var findUser = await _unitOfWork.UserRepository.FindUserByEmail(query.Email);
            var validFilter = new PaginationFilter(query.Filter.PageNumber, query.Filter.PageSize, query.Filter.Search, query.Filter.Type, query.Filter.Priority, query.Filter.StatusId, query.Filter.UserId);
            
            var cacheData = _cacheService.GetData<List<ProjectDTO>>($"ProjectDTO:{findUser.Email}");
            if(cacheData == null)
            {
                var projects = await _unitOfWork.ProjectRepository.GetProjectList(findUser.Id);
                cacheData = _mapper.Map<List<ProjectDTO>>(projects);
                var expireTime = DateTimeOffset.Now.AddSeconds(30);
                _cacheService.SetData<List<ProjectDTO>>($"ProjectDTO:{findUser.Email}", cacheData, expireTime);
            }
            var projectsDto = cacheData;
            if (!String.IsNullOrEmpty(query.Filter.Search))
            {
                projectsDto = cacheData.Where(x => x.Name.Contains(query.Filter.Search)).OrderByDescending(x => x.Id)
                             .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                             .Take(validFilter.PageSize).ToList();
                return (projectsDto, validFilter, projectsDto.Count());
            }
            projectsDto = cacheData.OrderByDescending(x => x.Id)
                         .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                         .Take(validFilter.PageSize).ToList();
            return (projectsDto, validFilter, cacheData.Count());

            
        }
    }
}
