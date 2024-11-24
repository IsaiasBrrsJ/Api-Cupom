using Coupon.Core.Repositories;

namespace Coupon.Infrastructure.Persistence.Repositories
{
    public class CouponRepositories : ICouponRepositories
    {
        private readonly CouponContextDb _dbContext;

        public CouponRepositories(CouponContextDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddAsync(Core.Entities.Coupon.Coupon client)
        {
            await _dbContext.AddAsync(client);

            return client.Id;
        }

        public Task DeleteLogicalAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Entities.Coupon.Coupon>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Entities.Coupon.Coupon>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Core.Entities.Coupon.Coupon client)
        {
            throw new NotImplementedException();
        }
    }
}
