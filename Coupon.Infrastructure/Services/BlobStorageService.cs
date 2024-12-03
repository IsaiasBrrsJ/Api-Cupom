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
        private readonly string _CONNFING = default!;
        private readonly string _CONTAINERNAME = default!;
        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;

             _CONNFING = _configuration["BlobConnection:Blob"]!;
             _CONTAINERNAME = _configuration["BlobConnection:PNG"]!;
        }

        public async Task<bool> DeletePhoto(Guid userId)
        {
            var blobServiceClient = new BlobServiceClient(_CONNFING);


            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_CONTAINERNAME);

            var blobName = $"{userId}.png";

            var blobClient = blobContainerClient.GetBlobClient(blobName);


            if (await blobClient.ExistsAsync())
            {
                await blobClient.DeleteIfExistsAsync();
                return true;
            }


            return false;
        }

        public async Task<IFormFile> DownloadPhoto(Guid userId)
        {
            throw new InvalidCastException();
        }

        public async Task<(string blobUrl, string fileName)> UploadPhoto(IFormFile file, Guid userId)
        {
            

            var blobServiceClient = new BlobServiceClient(_CONNFING);
            var containerClient = blobServiceClient.GetBlobContainerClient(_CONTAINERNAME);
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
