using Coupon.Core.Repositories;
using Coupon.Core.Services;

namespace Coupon.Application.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepositories _couponRepositories;
        private readonly IUnitOfWork _unitOfWork;

        public CouponService(ICouponRepositories couponRepositories, IUnitOfWork unitOfWork)
        {
            _couponRepositories = couponRepositories;
            _unitOfWork = unitOfWork;
        }

        public Task DeactivateCoupon(Guid coupon)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Entities.Coupon.Coupon>> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Entities.Coupon.Coupon>> GetClientById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> InsertCoupon(Core.Entities.Coupon.Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCoupon(Core.Entities.Coupon.Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
