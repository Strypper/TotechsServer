using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Intranet.Services.IServices
{
    public interface IMediaService
    {
        public bool IsImage(IFormFile file);
        public Task<Tuple<bool, string>> UploadFileToStorage(Stream fileStream, string fileName);
        public Task<Tuple<bool, string>> UploadAvatarToStorage(Stream fileStream, string fileName);
        public Task<List<string>> GetThumbNailUrls();
    }
}
