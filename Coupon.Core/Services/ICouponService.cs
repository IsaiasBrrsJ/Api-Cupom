using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Coupon;
using Microsoft.AspNetCore.Http;

namespace Coupon.Core.Services
{
    public interface ICouponService
    {
        Task UpdateCoupon(Entities.Coupon.Coupon coupon);
        Task<IEnumerable<Entities.Coupon.Coupon>> GetAllClients();
        Task<Entities.Coupon.Coupon> GetClientById(Guid id);
        Task<(string blobUrl, string fileName)> SendImageToBlobStorage(IFormFile photo, Guid idCoupon);
    }
}
