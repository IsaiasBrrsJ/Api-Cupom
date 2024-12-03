using Microsoft.AspNetCore.Http;

namespace Coupon.Core.Services
{
    public interface IBlobStorageService
    {
        Task<(string blobUrl, string fileName)> UploadPhoto(IFormFile file, Guid userId);

        Task<IFormFile> DownloadPhoto(Guid userId);

        Task<bool> DeletePhoto(Guid userId);
    }
}
