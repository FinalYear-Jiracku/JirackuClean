using AutoMapper;
using Firebase.Auth;
using Firebase.Storage;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities;
using User = TaskServices.Domain.Entities.User;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class UpdateIssueHandler : IRequestHandler<UpdateIssueCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseService _firebaseService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public UpdateIssueHandler(IUnitOfWork unitOfWork, IFirebaseService firebaseService, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _firebaseService = firebaseService;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<int> Handle(UpdateIssueCommand command, CancellationToken cancellationToken)
        {
            var issue = await _unitOfWork.IssueRepository.GetIssueById(command.Id);
            var user = await _unitOfWork.UserRepository.FindUserById(command.UserId);
            if (issue == null)
            {
                return default;
            }
            var newAttachments = new List<Attachment>();
            var existingAttachments = issue.Attachments.ToList();
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            if (issue.SprintId != command.SprintId)
            {
                var statusToDo = await _unitOfWork.StatusRepository.GetStatusToDo(command.SprintId);
                if(command.Files == null)
                {
                    issue.Name = command.Name;
                    issue.Description = command.Description;
                    issue.Type = command.Type;
                    issue.Priority = command.Priority;
                    issue.StoryPoint = command.StoryPoint;
                    issue.StartDate = command.StartDate;
                    issue.DueDate = command.DueDate;
                    issue.StatusId = statusToDo.Id;
                    issue.SprintId = command.SprintId;
                    issue.UpdatedBy = command.UpdatedBy;
                    issue.UpdatedAt = DateTimeOffset.Now;
                    if (issue.User == null && command.UserId == 0)
                    {
                        issue.User = null;
                    }
                    if (issue.User != null && issue.User.Id != command.UserId)
                    {
                        issue.User = user;
                    }
                    if (issue.User == null && command.UserId != 0)
                    {
                        issue.User = user;
                    }

                    var attachmentsToAdd1FileNull = newAttachments.Where(newAttachment => !existingAttachments.Any(existingAttachment => existingAttachment.FileName == newAttachment.FileName && existingAttachment.FileType == newAttachment.FileType)).ToList();
                    var attachmentsToRemove1FileNull = existingAttachments.Where(existingAttachment => !newAttachments.Any(newAttachment => newAttachment.FileName == existingAttachment.FileName && newAttachment.FileType == existingAttachment.FileType)).ToList();

                    foreach (var attachment in attachmentsToAdd1FileNull)
                    {
                        await _unitOfWork.Repository<Attachment>().AddAsync(attachment);
                    }
                    foreach (var attachment in attachmentsToRemove1FileNull)
                    {
                        await _unitOfWork.Repository<Attachment>().DeleteAsync(attachment);
                    }

                    await _unitOfWork.Repository<Issue>().UpdateAsync(issue);
                    issue.AddDomainEvent(new IssueUpdatedEvent(issue));
                    await _unitOfWork.Save(cancellationToken);
                    //var issuesFileNull = await _unitOfWork.IssueRepository.GetIssueListBySprintId(command.SprintId);
                    //var issuesDtoFileNull = _mapper.Map<List<IssueDTO>>(issuesFileNull);
                    //_cacheService.SetData<List<IssueDTO>>($"IssueDTO?sprintId={command.SprintId}", issuesDtoFileNull, expireTime);
                    return await Task.FromResult(0);
                }
                foreach (var file in command.Files)
                {
                    var document = await _firebaseService.CreateImage(file);
                    var fileType = GetFileType(file.FileName);
                    var attachment = new Attachment
                    {
                        FileName = document,
                        FileType = fileType,
                        Issue = issue
                    };
                    newAttachments.Add(attachment);
                }

                issue.Name = command.Name;
                issue.Description = command.Description;
                issue.Type = command.Type;
                issue.Priority = command.Priority;
                issue.StoryPoint = command.StoryPoint;
                issue.StartDate = command.StartDate;
                issue.DueDate = command.DueDate;
                issue.StatusId = statusToDo.Id;
                issue.SprintId = command.SprintId;
                issue.UpdatedBy = command.UpdatedBy;
                issue.UpdatedAt = DateTimeOffset.Now;
                if (issue.User == null && command.UserId == 0)
                {
                    issue.User = null;
                }
                if (issue.User != null && issue.User.Id != command.UserId)
                {
                    issue.User = user;
                }
                if (issue.User == null && command.UserId != 0)
                {
                    issue.User = user;
                }

                var attachmentsToAdd1 = newAttachments.Where(newAttachment => !existingAttachments.Any(existingAttachment => existingAttachment.FileName == newAttachment.FileName && existingAttachment.FileType == newAttachment.FileType)).ToList();
                var attachmentsToRemove1 = existingAttachments.Where(existingAttachment => !newAttachments.Any(newAttachment => newAttachment.FileName == existingAttachment.FileName && newAttachment.FileType == existingAttachment.FileType)).ToList();
                
                foreach (var attachment in attachmentsToAdd1)
                {
                    await _unitOfWork.Repository<Attachment>().AddAsync(attachment);
                }
                foreach (var attachment in attachmentsToRemove1)
                {
                    await _unitOfWork.Repository<Attachment>().DeleteAsync(attachment);
                }
                await _unitOfWork.Repository<Issue>().UpdateAsync(issue);
                issue.AddDomainEvent(new IssueUpdatedEvent(issue));
                await _unitOfWork.Save(cancellationToken);
                //var issues = await _unitOfWork.IssueRepository.GetIssueListBySprintId(command.SprintId);
                //var issuesDto = _mapper.Map<List<IssueDTO>>(issues);
                //_cacheService.SetData<List<IssueDTO>>($"IssueDTO?sprintId={command.SprintId}&pageNumber=1&search=", issuesDto, expireTime);
                return await Task.FromResult(0);
            }

            if (command.Files == null)
            {
                issue.Name = command.Name;
                issue.Description = command.Description;
                issue.Type = command.Type;
                issue.Priority = command.Priority;
                issue.StoryPoint = command.StoryPoint;
                issue.StartDate = command.StartDate;
                issue.DueDate = command.DueDate;
                issue.StatusId = command.StatusId;
                issue.UpdatedBy = command.UpdatedBy;
                issue.UpdatedAt = DateTimeOffset.Now;
                if(issue.User == null && command.UserId == 0)
                {
                    issue.User = null;
                }
                if(issue.User != null && issue.User.Id != command.UserId)
                {
                    issue.User = user;
                }
                if(issue.User == null && command.UserId != 0)
                {
                    issue.User = user;
                }

                var attachmentsToAdd1FileNull = newAttachments.Where(newAttachment => !existingAttachments.Any(existingAttachment => existingAttachment.FileName == newAttachment.FileName && existingAttachment.FileType == newAttachment.FileType)).ToList();
                var attachmentsToRemove1FileNull = existingAttachments.Where(existingAttachment => !newAttachments.Any(newAttachment => newAttachment.FileName == existingAttachment.FileName && newAttachment.FileType == existingAttachment.FileType)).ToList();

                foreach (var attachment in attachmentsToAdd1FileNull)
                {
                    await _unitOfWork.Repository<Attachment>().AddAsync(attachment);
                }
                foreach (var attachment in attachmentsToRemove1FileNull)
                {
                    await _unitOfWork.Repository<Attachment>().DeleteAsync(attachment);
                }

                await _unitOfWork.Repository<Issue>().UpdateAsync(issue);
                issue.AddDomainEvent(new IssueUpdatedEvent(issue));
                await _unitOfWork.Save(cancellationToken);
                //var issuesFileNull = await _unitOfWork.IssueRepository.GetIssueListBySprintId(command.SprintId);
                //var issuesDtoFileNull = _mapper.Map<List<IssueDTO>>(issuesFileNull);
                //_cacheService.SetData<List<IssueDTO>>($"IssueDTO?sprintId={command.SprintId}", issuesDtoFileNull, expireTime);
                return await Task.FromResult(0);
            }

            foreach (var file in command.Files)
            {
                var document = await _firebaseService.CreateImage(file);
                var fileType = GetFileType(file.FileName);
                var attachment = new Attachment
                {
                    FileName = document,
                    FileType = fileType,
                    Issue = issue
                };
                newAttachments.Add(attachment);
            }
           
            issue.Name = command.Name;
            issue.Description = command.Description;
            issue.Type = command.Type;
            issue.Priority = command.Priority;
            issue.StoryPoint = command.StoryPoint;
            issue.StartDate = command.StartDate;
            issue.DueDate = command.DueDate;
            issue.StatusId = command.StatusId;
            issue.UpdatedBy = command.UpdatedBy;
            issue.UpdatedAt = DateTimeOffset.Now;

            if (issue.User == null && command.UserId == 0)
            {
                issue.User = null;
            }
            if (issue.User != null && issue.User.Id != command.UserId)
            {
                issue.User = user;
            }
            if (issue.User == null && command.UserId != 0)
            {
                issue.User = user;
            }

            var attachmentsToAdd = newAttachments.Where(newAttachment =>!existingAttachments.Any(existingAttachment => existingAttachment.FileName == newAttachment.FileName && existingAttachment.FileType == newAttachment.FileType)).ToList();
            var attachmentsToRemove = existingAttachments.Where(existingAttachment => !newAttachments.Any(newAttachment => newAttachment.FileName == existingAttachment.FileName && newAttachment.FileType == existingAttachment.FileType)).ToList();

            foreach (var attachment in attachmentsToAdd)
            {
                await _unitOfWork.Repository<Attachment>().AddAsync(attachment);
            }

            foreach (var attachment in attachmentsToRemove)
            {
                await _unitOfWork.Repository<Attachment>().DeleteAsync(attachment);
            }
            await _unitOfWork.Repository<Issue>().UpdateAsync(issue);
            issue.AddDomainEvent(new IssueUpdatedEvent(issue));
            await _unitOfWork.Save(cancellationToken);
            //var issueList = await _unitOfWork.IssueRepository.GetIssueListBySprintId(command.SprintId);
            //var issueDtoList = _mapper.Map<List<IssueDTO>>(issueList);
            //_cacheService.SetData<List<IssueDTO>>($"IssueDTO?sprintId={command.SprintId}", issueDtoList, expireTime);
            return await Task.FromResult(0);
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
