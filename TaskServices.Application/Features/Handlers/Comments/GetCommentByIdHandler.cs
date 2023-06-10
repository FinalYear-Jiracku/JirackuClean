using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Comments;
using TaskServices.Application.Interfaces;

namespace TaskServices.Application.Features.Handlers.Comments
{
    public class GetCommentByIdHandler : IRequestHandler<GetCommentByIdQuery, CommentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetCommentByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<CommentDTO> Handle(GetCommentByIdQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<CommentDTO>($"CommentDTO{query.Id}");
            if (cacheData != null)
            {
                return cacheData;
            }
            var comment = await _unitOfWork.CommentRepository.GetCommentById(query.Id);
            var commentDto = _mapper.Map<CommentDTO>(comment);
            if (commentDto == null)
            {
                return null;
            }
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<CommentDTO>($"CommentDTO{commentDto.Id}", commentDto, expireTime);
            return commentDto;
        }
    }
}
