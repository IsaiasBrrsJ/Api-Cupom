using Coupon.Core.Services;
using Microsoft.AspNetCore.Http;


namespace Coupon.Application.Services.Coupons
{
    public class CouponService : ICouponService
    {
        private readonly IBlobStorageService _blobStorageService;
        public CouponService(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        public async Task<(string blobUrl, string fileName)> SendImageToBlobStorage(IFormFile photo, Guid idCoupon)
        {
            var infoBlob = await _blobStorageService.UploadPhoto(photo!, idCoupon);

            return infoBlob;
        }

        public async Task<bool> DeleteImage(Guid idCoupon)
        {
            return await _blobStorageService.DeletePhoto(idCoupon);
        }
    }
}
