using Coupon.Core.Entities.Coupon;
using Microsoft.AspNetCore.Http;

namespace Coupon.Core.Services
{
    public interface ICouponService
    {
        Task<Guid> InsertCoupon(Entities.Coupon.Coupon coupon, IFormFile photo);
        Task UpdateCoupon(Entities.Coupon.Coupon coupon);
        Task DeactivateCoupon(Guid coupon);
        Task<IEnumerable<Entities.Coupon.Coupon>> GetAllClients();
        Task<IEnumerable<Entities.Coupon.Coupon>> GetClientById(Guid id);
        internal Task<(string blobUrl, string fileName)> SendImageToBlobStorage(IFormFile photo, Guid idCoupon); 
    }
}
