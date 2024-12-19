namespace Coupon.Core.Repositories
{
    public interface ICouponRepositories
    {
        Task<Guid> AddAsync(Entities.Coupon.Coupon client);
        Task<IEnumerable<Entities.Coupon.Coupon>> GetAllAsync();
        Task<Entities.Coupon.Coupon> GetByIdAsync(Guid id);
        void UpdateAsync(Entities.Coupon.Coupon coupon);
        Task DeleteLogicalAsync(Guid id);
        Task<Entities.Coupon.Coupon> FindByIdEntityAsync(Guid id);
    }
}
