using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Azure.Core.HttpHeader;

namespace Coupon.Infrastructure.Persistence.Repositories;

public class DiscountRepositories : IDiscountRepositories
{
    private readonly CouponContextDb _dbContext;
    private readonly IConfiguration _configuration;

    public DiscountRepositories(
        CouponContextDb dbContext, 
        IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<Guid> Create(Discount descount)
    {
       await _dbContext.Descounts.AddAsync(descount);

        return descount.Id; 
    }

    public async Task<Discount> FindByIdEntity(Guid id)
    {
        var result = await _dbContext.Descounts.FindAsync(id);

        return result!;
    }

    public async Task<Discount> GetByIdDapper(Guid id)
    {
        var connectionString = _configuration.GetConnectionString("BdEstudos");

        var query = @"SELECT 
                        TipoDesconto, 
                        PercentDescount, 
                        CreateAt, 
                        IsActive
                    FROM dbo.Descounts
                    WHERE  Id = @IdDiscount;";

        var parameters = new DynamicParameters();
        parameters.Add("@IdDiscount", id);


        using (var conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();

          var result =  await conn.QuerySingleOrDefaultAsync<Discount>(query, parameters);

            return result!;
        }
    }

    public void Update(Discount descount)
    {
         _dbContext.Entry(descount).State = EntityState.Modified;
    }
}
