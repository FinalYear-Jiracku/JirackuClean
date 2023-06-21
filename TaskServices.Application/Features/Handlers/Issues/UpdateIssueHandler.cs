using Firebase.Auth;
using Firebase.Storage;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class UpdateIssueHandler : IRequestHandler<UpdateIssueCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseService _firebaseService;
        public UpdateIssueHandler(IUnitOfWork unitOfWork, IFirebaseService firebaseService)
        {
            _unitOfWork = unitOfWork;
            _firebaseService = firebaseService;
        }
        public async Task<int> Handle(UpdateIssueCommand command, CancellationToken cancellationToken)
        {
            var issue = await _unitOfWork.IssueRepository.GetIssueById(command.Id);
            if (issue == null)
            {
                return default;
            }
            var newAttachments = new List<Attachment>();
            var existingAttachments = issue.Attachments.ToList();
            if (issue.SprintId != command.SprintId)
            {
                var statusToDo = await _unitOfWork.StatusRepository.GetStatusToDo(command.SprintId);
                foreach (var file in command.Files)
                {
                    var document = await _firebaseService.CreateImage(file);
                    var attachment = new Attachment
                    {
                        FileName = document,
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
                return await _unitOfWork.Save(cancellationToken);
            }

            foreach (var file in command.Files)
            {
                var document = await _firebaseService.CreateImage(file);
                var attachment = new Attachment
                {
                    FileName = document,
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
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
