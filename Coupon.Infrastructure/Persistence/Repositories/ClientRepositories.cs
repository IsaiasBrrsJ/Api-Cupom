using Coupon.Core.Entities.Client;
using Coupon.Core.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task DeleteLogicalAsync(Guid id)
        {
            string sql = @"UPDATE FROM dbo.Clients
                        SET IsActive = 0
                        WHERE Id = @Id";

            _dbContext.Clients.FromSqlRaw(sql, id);

           await Task.CompletedTask;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
           return await _dbContext.Clients.ToListAsync();
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            return await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == id) ?? null!;
        }

    }
}
