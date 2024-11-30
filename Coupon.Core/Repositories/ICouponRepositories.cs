namespace Coupon.Core.Repositories
{
    public interface ICouponRepositories
    {
        Task<Guid> AddAsync(Entities.Coupon.Coupon client);
        Task<IEnumerable<Entities.Coupon.Coupon>> GetAllAsync();
        Task<Entities.Coupon.Coupon> GetByIdAsync(Guid id);
        Task UpdateAsync(Entities.Coupon.Coupon client);
        Task DeleteLogicalAsync(Guid id);
    }
}
