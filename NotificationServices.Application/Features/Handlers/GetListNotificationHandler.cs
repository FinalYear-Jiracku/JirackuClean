using AutoMapper;
using MediatR;
using NotificationServices.Application.DTOs;
using NotificationServices.Application.Features.Queries;
using NotificationServices.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Features.Handlers
{
    public class GetListNotificationHandler : IRequestHandler<GetListNotificationQuery, List<NotificationDTO>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;


        public GetListNotificationHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }
        public async Task<List<NotificationDTO>> Handle(GetListNotificationQuery query, CancellationToken cancellationToken)
        {
            var listNoti = await _notificationRepository.GetNotificationByProjectId(query.Id);
            var listNotiDTO = _mapper.Map<List<NotificationDTO>>(listNoti).OrderByDescending(x => x.CreatedAt).ToList();
            return listNotiDTO;
        }
    }
}
