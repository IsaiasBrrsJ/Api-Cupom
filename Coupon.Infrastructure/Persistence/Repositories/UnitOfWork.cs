using Coupon.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupon.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CouponContextDb _dbContext;
        public UnitOfWork(CouponContextDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Commit()
        {
            int umaOuMaisLinhasAfetadas = 0;

            return await 
                _dbContext
                .SaveChangesAsync() > umaOuMaisLinhasAfetadas;
        }

        public Task Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
