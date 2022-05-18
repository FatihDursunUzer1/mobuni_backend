using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.Infrastructure.Storage
{
    public class AzureStorage:IStorage
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;
        public AzureStorage(IConfiguration configuration,IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _blobServiceClient = new BlobServiceClient(_configuration["BlobConnectionString"]);
        }

        private async Task<string> UploadFile(IFormFile files, string pathName)
        {
           var userId=_contextAccessor.HttpContext.Items["UserId"]?.ToString();
            string connectionString = _configuration["BlobConnectionString"];
            try
            {
                var blobContainer = _blobServiceClient.GetBlobContainerClient("root");
                await blobContainer.CreateIfNotExistsAsync();
                var blobClient = blobContainer.GetBlobClient(userId+"/"+pathName + "/"+files.FileName);

                await blobClient.UploadAsync(files.OpenReadStream());

                // Verify we uploaded some content
                BlobProperties properties = await blobClient.GetPropertiesAsync();

                return blobClient.BlobContainerName + "/"+blobClient.Name;
            }
            catch(Exception ex)
            {
                throw;
            }
           
        }

        public async Task<byte[]> GetFile(string fullpath)
        {
            var documentPath = fullpath.Split('/');
            var blobContainer = _blobServiceClient.GetBlobContainerClient(documentPath[0]);

            var blobClient = blobContainer.GetBlobClient(documentPath[1] + "/" + documentPath[2]);
            var downloadContent = await blobClient.DownloadAsync();
            using (MemoryStream ms = new MemoryStream())
            {
                await downloadContent.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public async Task<string> UploadQuestionImage(IFormFile files)
        {
             return await UploadFile(files,"question");
        }

        public async Task<string> UploadActivityImage(IFormFile files)
        {
            return await UploadFile(files, "activity");
        }

        public async Task<string> UploadProfileImage(IFormFile files)
        {
             return await UploadFile(files, "profile");
        }
    }

}
