using Coupon.Core.Entities.Client;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Coupon.Infrastructure.Persistence.Repositories
{
    public class ClientRepositories : IClientRepositories
    {

        private readonly CouponContextDb _dbContext;
        private readonly IConfiguration _configurations;
        public ClientRepositories(CouponContextDb dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configurations = configuration;
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

        public async Task<Client> GetByIdDapperAsync(Guid id)
        {
            var connectionString = _configurations.GetConnectionString("BdEstudos");

            var query = @" SELECT Id, Name, Email, PhoneNumber, ClientType, IsActive
                            FROM dbo.Clients
                            WHERE Id = @IdClient";


            var parameters = new DynamicParameters();
            parameters.Add("@IdClient", id);

            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                var result = await conn.QuerySingleOrDefaultAsync<Client>(query, parameters);

                return result!;
            }
        }
    }
}
