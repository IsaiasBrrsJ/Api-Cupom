using Coupon.Core.Entities.Client;
using Coupon.Core.Entities.Coupon;

namespace Coupon.Core.Services
{
    public interface ICouponRepositories
    {
        Task<Guid> AddAsync(Entities.Coupon.Coupon client);
        Task<IEnumerable<Entities.Coupon.Coupon>> GetAllAsync();
        Task<IEnumerable<Entities.Coupon.Coupon>> GetByIdAsync(Guid id);
        Task UpdateAsync(Entities.Coupon.Coupon client);
        Task DeleteLogicalAsync(Guid id);
    }
}
