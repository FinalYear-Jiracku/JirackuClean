using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Issues;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities.Enums;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class StatisUserHandler : IRequestHandler<StatisUserQuery, List<StatisUserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public StatisUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<List<StatisUserDTO>> Handle(StatisUserQuery query, CancellationToken cancellationToken)
        {
            var sprint = await _unitOfWork.SprintRepository.GetSprintById(query.Id);
            if (sprint == null)
            {
                return null;
            }
            var userIssueStatusList = new List<StatisUserDTO>();

            foreach (var userProject in sprint.Project.UserProjects)
            {
                var user = userProject.User;

                var userStatusStats = new StatisUserDTO
                {
                   
                    Email = user.Email,
                    StatusCounts = new List<StatusCountDTO>()
                };

                foreach (var status in sprint.Statuses)
                {
                    var count = sprint.Issues.Count(issue => issue.User == user && issue.Status == status);

                    userStatusStats.StatusCounts.Add(new StatusCountDTO
                    {
                        StatusName = status.Name,
                        IssueCount = count
                    });
                }

                userIssueStatusList.Add(userStatusStats);
            }

            return userIssueStatusList;
        }
    }
}
