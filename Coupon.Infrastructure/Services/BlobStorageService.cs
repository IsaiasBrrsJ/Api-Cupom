using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Coupon.Infrastructure.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IConfiguration _configuration;
        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  async Task<IFormFile> DownloadPhoto(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<(string blobUrl, string fileName)> UploadPhoto(IFormFile file, Guid userId)
        {
            var connString = _configuration["BlobConnection:Blob"];
            var containerName = _configuration["BlobConnection:BlobPNG"];

            var blobServiceClient = new BlobServiceClient(connString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

            var blobName = $"{userId}.png";

            var blobClient = containerClient.GetBlobClient(blobName);


            await using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

         
            var blobUrl = blobClient.Uri.ToString();


            return (blobUrl, blobName);

        }
    }
}
