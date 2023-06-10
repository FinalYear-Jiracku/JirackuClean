using Firebase.Auth;
using Firebase.Storage;
using MediaInfoDotNet;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class UpdateIssueHandler : IRequestHandler<UpdateIssueCommand, int>
    {
        private static string apiKey = "AIzaSyAOieBJUgTWNgzdlrQToC309z4d4CmCnxY";
        private static string Bucket = "jiracku.appspot.com";
        private static string AuthEmail = "dinhgiabao1120@gmail.com";
        private static string AuthPassword = "dinhgiabao";
        private readonly IUnitOfWork _unitOfWork;
        public UpdateIssueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
