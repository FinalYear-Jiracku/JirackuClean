using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Notes;
using TaskServices.Application.Interfaces;

namespace TaskServices.Application.Features.Handlers.Notes
{
    public class GetNoteByIdHandler : IRequestHandler<GetNoteByIdQuery, NoteDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetNoteByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<NoteDTO> Handle(GetNoteByIdQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<NoteDTO>($"NoteDTO{query.Id}");
            if (cacheData != null)
            {
                return cacheData;
            }
            var note = await _unitOfWork.NoteRepository.GetNoteById(query.Id);
            var noteDto = _mapper.Map<NoteDTO>(note);
            if (noteDto == null)
            {
                return null;
            }
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<NoteDTO>($"NoteDTO{noteDto.Id}", noteDto, expireTime);
            return noteDto;
        }
    }
}
