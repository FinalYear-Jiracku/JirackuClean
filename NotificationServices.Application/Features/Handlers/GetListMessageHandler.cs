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
    public class GetListMessageHandler : IRequestHandler<GetListMessageQuery, List<MessageDTO>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;


        public GetListMessageHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }
        public async Task<List<MessageDTO>> Handle(GetListMessageQuery query, CancellationToken cancellationToken)
        {
            var listMessage = await _messageRepository.GetMessageByProjectId(query.Id);
            var listMessageDTO = _mapper.Map<List<MessageDTO>>(listMessage).ToList();
            return listMessageDTO;
        }
    }
}
