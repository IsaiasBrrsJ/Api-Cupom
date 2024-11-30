using Coupon.Core.BaseResult;
using Microsoft.AspNetCore.Http;

namespace Coupon.Core.Services
{
    public interface ICouponService
    {
        Task<ResultViewModel> InsertCoupon(Entities.Coupon.Coupon coupon, IFormFile? photo);
        Task UpdateCoupon(Entities.Coupon.Coupon coupon);
        Task DeactivateCoupon(Guid coupon, string reason, string @operator);
        Task<IEnumerable<Entities.Coupon.Coupon>> GetAllClients();
        Task<Entities.Coupon.Coupon> GetClientById(Guid id);
        Task<(string blobUrl, string fileName)> SendImageToBlobStorage(IFormFile photo, Guid idCoupon);
    }
}
