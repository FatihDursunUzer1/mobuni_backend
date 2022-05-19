using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore
{
    public interface IStorage
    {
        public Task<string> UploadQuestionImage(IFormFile files,int id);
        public Task<string> UploadActivityImage(IFormFile files,int id);
        public Task<string> UploadProfileImage(IFormFile files);
        public Task<byte[]> GetFile(string fileName);
    }
}
