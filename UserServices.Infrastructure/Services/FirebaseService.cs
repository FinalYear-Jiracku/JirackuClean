using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.Interfaces.IServices;

namespace UserServices.Infrastructure.Services
{
    public class FirebaseService : IFirebaseService
    {
        private static string apiKey = "AIzaSyAOieBJUgTWNgzdlrQToC309z4d4CmCnxY";
        private static string Bucket = "jiracku.appspot.com";
        private static string AuthEmail = "dinhgiabao1120@gmail.com";
        private static string AuthPassword = "dinhgiabao";
        public async Task<string> CreateImage(IFormFile file)
        {
            if (!IsValidFileType(file.FileName))
            {
                return default;
            }
            var name = file.FileName;
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
            return document;
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
            List<string> validExtensions = new List<string> { ".pdf", ".xlsx", ".png", ".mp4", ".docx", ".doc", ".csv", ".jpg", ".jpge" };
            return validExtensions.Contains(fileExtension);
        }
    }
}
