using Firebase.Auth;
using Firebase.Storage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public UpdateSubIssueHandler(IUnitOfWork unitOfWork, IFirebaseService firebaseService)
        {
            _unitOfWork = unitOfWork;
            _firebaseService = firebaseService;
        }
        public async Task<int> Handle(UpdateSubIssueCommand command, CancellationToken cancellationToken)
        {
            var subIssue = await _unitOfWork.SubIssueRepository.GetSubIssueById(command.Id);
            if (subIssue == null)
            {
                return default;
            }
            var newAttachments = new List<Attachment>();
            var existingAttachments = subIssue.Attachments.ToList();
            foreach (var file in command.Files)
            {
                var document = await _firebaseService.CreateImage(file);
                var attachment = new Attachment
                {
                    FileName = document,
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
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
