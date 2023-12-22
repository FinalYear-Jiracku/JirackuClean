using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Infrastructure.Utils;

namespace TaskServices.Infrastructure.Services
{
    public class FireBaseService : IFirebaseService
    {
        private readonly FirebaseConfiguration _fireConfig;
        public FireBaseService(IOptions<FirebaseConfiguration> fireConfig)
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
