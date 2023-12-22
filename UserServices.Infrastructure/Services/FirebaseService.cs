using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.Interfaces.IServices;
using UserServices.Application.Utils;

namespace UserServices.Infrastructure.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseConfiguration _fireConfig;
        public FirebaseService(IOptions<FirebaseConfiguration> fireConfig)
        {
            _fireConfig = fireConfig.Value;
        }
        public async Task<string> CreateImage(IFormFile file)
        {
            var name = file.FileName;
            var fileStream = file.OpenReadStream();
            string document = "";
            var auth = new FirebaseAuthProvider(new FirebaseConfig(_fireConfig.ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(_fireConfig.AuthEmail, _fireConfig.AuthPassword);
            var cancel = new CancellationTokenSource();
            var task = new FirebaseStorage(
                _fireConfig.Bucket,
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
    }
}
