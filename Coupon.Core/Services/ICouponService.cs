namespace Coupon.Core.Services
{
    public interface ICouponService
    {
        Task<Guid> InsertCoupon(Entities.Coupon.Coupon coupon);
        Task UpdateCoupon(Entities.Coupon.Coupon coupon);
        Task DeactivateCoupon(Guid coupon);
        Task<IEnumerable<Entities.Coupon.Coupon>> GetAllClients();
        Task<IEnumerable<Entities.Coupon.Coupon>> GetClientById(Guid id);
    }
}
