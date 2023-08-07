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
    public class GetMessageDetailHandler : IRequestHandler<GetMessageDetailQuery,MessageDTO>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public GetMessageDetailHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }
        public async Task<MessageDTO> Handle(GetMessageDetailQuery query, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetMessageDetail(query.Id);
            var messageDTO = _mapper.Map<MessageDTO>(message);
            return messageDTO;
        }
    }
}
