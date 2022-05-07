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

        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;
        public AzureStorage(IConfiguration configuration)
        {
            _configuration = configuration;
            _blobServiceClient = new BlobServiceClient(_configuration["BlobConnectionString"]);
        }

        public async Task UploadFile(IFormFile files)
        {
            
            string connectionString = _configuration["BlobConnectionString"];
            try
            {
                // Get a reference to a blob
                var blobContainer = _blobServiceClient.GetBlobContainerClient(_configuration["BlobContainerName"]);

                var blobClient = blobContainer.GetBlobClient(files.FileName);

                // Upload file data
                await blobClient.UploadAsync(files.OpenReadStream());

                // Verify we uploaded some content
                BlobProperties properties = await blobClient.GetPropertiesAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<byte[]> GetFile(string fileName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_configuration["BlobContainerName"]);

            var blobClient = blobContainer.GetBlobClient(fileName);
            var downloadContent = await blobClient.DownloadAsync();
            using (MemoryStream ms = new MemoryStream())
            {
                await downloadContent.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

    }

}
