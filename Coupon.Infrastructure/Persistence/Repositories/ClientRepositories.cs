using Coupon.Core.Entities.Client;
using Coupon.Core.Repositories;

namespace Coupon.Infrastructure.Persistence.Repositories
{
    public class ClientRepositories : IClientRepositories
    {

        private readonly CouponContextDb _dbContext;

        public ClientRepositories(CouponContextDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddAsync(Client client)
        {
            await _dbContext
                .Clients
                .AddAsync(client);

            return client.Id;
        }

        public Task DeleteLogicalAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
