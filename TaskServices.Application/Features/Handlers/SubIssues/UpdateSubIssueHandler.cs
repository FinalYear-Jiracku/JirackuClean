using AutoMapper;
using Firebase.Auth;
using Firebase.Storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Features.Commands.SubIssues;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.SubIssues
{
    public class UpdateSubIssueHandler : IRequestHandler<UpdateSubIssueCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseService _firebaseService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public UpdateSubIssueHandler(IUnitOfWork unitOfWork, IFirebaseService firebaseService, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _firebaseService = firebaseService;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<int> Handle(UpdateSubIssueCommand command, CancellationToken cancellationToken)
        {
            var subIssue = await _unitOfWork.SubIssueRepository.GetSubIssueById(command.Id);
            var user = await _unitOfWork.UserRepository.FindUserById(command.UserId);
            if (subIssue == null)
            {
                return default;
            }
            var newAttachments = new List<Attachment>();
            var existingAttachments = subIssue.Attachments.ToList();
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            if (command.Files == null)
            {
                subIssue.Name = command.Name;
                subIssue.Description = command.Description;
                subIssue.Type = command.Type;
                subIssue.Priority = command.Priority;
                subIssue.StoryPoint = command.StoryPoint;
                subIssue.StartDate = command.StartDate;
                subIssue.DueDate = command.DueDate;
                subIssue.StatusId = command.StatusId;
                subIssue.UpdatedBy = command.UpdatedBy;
                subIssue.UpdatedAt = DateTimeOffset.Now;
                if (subIssue.User == null && command.UserId == 0)
                {
                    subIssue.User = null;
                }
                if (subIssue.User != null && subIssue.User.Id != command.UserId)
                {
                    subIssue.User = user;
                }
                if (subIssue.User == null && command.UserId != 0)
                {
                    subIssue.User = user;
                }

                var attachmentsToAddNull = newAttachments.Where(newAttachment => !existingAttachments.Any(existingAttachment => existingAttachment.FileName == newAttachment.FileName && existingAttachment.FileType == newAttachment.FileType)).ToList();
                var attachmentsToRemoveNull = existingAttachments.Where(existingAttachment => !newAttachments.Any(newAttachment => newAttachment.FileName == existingAttachment.FileName && newAttachment.FileType == existingAttachment.FileType)).ToList();

                foreach (var attachment in attachmentsToAddNull)
                {
                    await _unitOfWork.Repository<Attachment>().AddAsync(attachment);
                }

                foreach (var attachment in attachmentsToRemoveNull)
                {
                    await _unitOfWork.Repository<Attachment>().DeleteAsync(attachment);
                }
                await _unitOfWork.Repository<SubIssue>().UpdateAsync(subIssue);
                subIssue.AddDomainEvent(new SubIssueUpdatedEvent(subIssue));
                var subIssuesFileNull = await _unitOfWork.SubIssueRepository.GetSubIssueListByIssueId(command.IssueId);
                var subIssuesDtoFileNull = _mapper.Map<List<SubIssueDTO>>(subIssuesFileNull);
                _cacheService.SetData<List<SubIssueDTO>>($"SubIssueDTO?sprintId={command.IssueId}", subIssuesDtoFileNull, expireTime);
                return await _unitOfWork.Save(cancellationToken);
            }
            foreach (var file in command.Files)
            {
                var document = await _firebaseService.CreateImage(file);
                var fileType = GetFileType(file.FileName);
                var attachment = new Attachment
                {
                    FileName = document,
                    FileType = fileType,
                    SubIssue = subIssue
                };
                newAttachments.Add(attachment);
            }
            subIssue.Name = command.Name;
            subIssue.Description = command.Description;
            subIssue.Type = command.Type;
            subIssue.Priority = command.Priority;
            subIssue.StoryPoint = command.StoryPoint;
            subIssue.StartDate = command.StartDate;
            subIssue.DueDate = command.DueDate;
            subIssue.StatusId = command.StatusId;
            subIssue.UpdatedBy = command.UpdatedBy;
            subIssue.UpdatedAt = DateTimeOffset.Now;
            if (subIssue.User == null && command.UserId == 0)
            {
                subIssue.User = null;
            }
            if (subIssue.User != null && subIssue.User.Id != command.UserId)
            {
                subIssue.User = user;
            }
            if (subIssue.User == null && command.UserId != 0)
            {
                subIssue.User = user;
            }

            var attachmentsToAdd = newAttachments.Where(newAttachment => !existingAttachments.Any(existingAttachment => existingAttachment.FileName == newAttachment.FileName && existingAttachment.FileType == newAttachment.FileType)).ToList();
            var attachmentsToRemove = existingAttachments.Where(existingAttachment => !newAttachments.Any(newAttachment => newAttachment.FileName == existingAttachment.FileName && newAttachment.FileType == existingAttachment.FileType)).ToList();

            foreach (var attachment in attachmentsToAdd)
            {
                await _unitOfWork.Repository<Attachment>().AddAsync(attachment);
            }

            foreach (var attachment in attachmentsToRemove)
            {
                await _unitOfWork.Repository<Attachment>().DeleteAsync(attachment);
            }
            await _unitOfWork.Repository<SubIssue>().UpdateAsync(subIssue);
            subIssue.AddDomainEvent(new SubIssueUpdatedEvent(subIssue));
            var subIssuesFile = await _unitOfWork.SubIssueRepository.GetSubIssueListByIssueId(command.IssueId);
            var subIssuesDtoFile = _mapper.Map<List<SubIssueDTO>>(subIssuesFile);
            _cacheService.SetData<List<SubIssueDTO>>($"SubIssueDTO?sprintId={command.IssueId}", subIssuesDtoFile, expireTime);
            return await _unitOfWork.Save(cancellationToken);
        }

        private string GetFileType(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName).ToLower();

            switch (fileExtension)
            {
                case ".pdf":
                    return "PDF";
                case ".xlsx":
                    return "XSLX";
                case ".png":
                    return "PNG";
                case ".jpeg":
                    return "JPEG";
                case ".jpg":
                    return "JPG";
                case ".mp4":
                    return "MP4";
                case ".docx":
                    return "DOCX";
                case ".doc":
                    return "DOC";
                case ".csv":
                    return "CSV";
                default:
                    return "Unknown";
            }
        }
    }
}
