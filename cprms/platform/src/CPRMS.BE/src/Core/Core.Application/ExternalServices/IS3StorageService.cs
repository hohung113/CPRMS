using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ExternalServices
{
    public interface IS3StorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder = "uploads");
        Task<bool> DeleteFileAsync(string fileKey);
    }
}
