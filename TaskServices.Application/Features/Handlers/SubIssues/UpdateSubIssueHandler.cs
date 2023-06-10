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
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.SubIssues
{
    public class UpdateSubIssueHandler : IRequestHandler<UpdateSubIssueCommand, int>
    {
        private static string apiKey = "AIzaSyAOieBJUgTWNgzdlrQToC309z4d4CmCnxY";
        private static string Bucket = "jiracku.appspot.com";
        private static string AuthEmail = "dinhgiabao1120@gmail.com";
        private static string AuthPassword = "dinhgiabao";
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSubIssueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                if (!IsValidFileType(file.FileName))
                {
                    return default;
                }
                var name = DateTime.Now.ToFileTime() + file.FileName;
                var fileType = GetFileType(file.FileName);
                var fileStream = file.OpenReadStream();
                string document = "";
                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
                var cancel = new CancellationTokenSource();
                var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("documents")
                    .Child(name)
                    .PutAsync(fileStream, cancel.Token);

                try
                {
                    var link = await task;
                    document = link;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error:{e.Message}");
                }
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
        private bool IsValidFileType(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName).ToLower();
            List<string> validExtensions = new List<string> { ".pdf", ".xlsx", ".png", ".mp4", ".docx", ".doc", ".csv" };
            return validExtensions.Contains(fileExtension);
        }
    }
}
