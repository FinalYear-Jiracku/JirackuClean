using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServices.Application.Interfaces.IServices
{
    public interface IFirebaseService
    {
        Task<string> CreateImage(IFormFile formFiles);
    }
}
