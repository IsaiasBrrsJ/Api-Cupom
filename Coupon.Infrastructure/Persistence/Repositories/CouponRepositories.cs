using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Coupon.Infrastructure.Persistence.Repositories
{
    public class CouponRepositories : ICouponRepositories
    {
        private readonly CouponContextDb _dbContext;
        private readonly IConfiguration _configurations;
        public CouponRepositories(CouponContextDb dbContext, IConfiguration configurations)
        {
            _dbContext = dbContext;
            _configurations = configurations;
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

        public async Task<Core.Entities.Coupon.Coupon> GetByIdAsync(Guid id)
        {
            var connectionString = _configurations.GetConnectionString("BdEstudos");

            var query = @"  SELECT TypeCoupon, Price, ValidAt, IsExpired, IsActive, EventDate, MaxCoupon, CreationDate, FileName, AddedOn, BlobUrl, ContentType
                            FROM Coupon
                            INNER JOIN Photos
                            ON Coupon.Id = Photos.CouponId
                            Where Coupon.Id = @Id;";



            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();


                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);


                var result = await conn.QueryAsync<Core.Entities.Coupon.Coupon, Photo, Core.Entities.Coupon.Coupon>(
                 query,
                       (coupon, photo) =>
                       {

                         coupon.Photo = photo;
                         return coupon;
                       },
                 parameters,
                 splitOn: "FileName"
                 );

                return result.SingleOrDefault()!;

            }

        }

        public Task UpdateAsync(Core.Entities.Coupon.Coupon client)
        {
            throw new NotImplementedException();
        }
    }
}
