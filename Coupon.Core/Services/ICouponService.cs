using Microsoft.AspNetCore.Http;

namespace Coupon.Core.Services;

public interface ICouponService
{
    Task<(string blobUrl, string fileName)> SendImageToBlobStorage(IFormFile photo, Guid idCoupon);

    Task<bool> DeleteImage(Guid idCoupon);
}
