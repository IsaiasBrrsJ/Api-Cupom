using Coupon.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System;
namespace Coupon.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CouponContextDb _dbContext;
        private IDbContextTransaction _dbContextTransaction = default!;

        public UnitOfWork(CouponContextDb dbContext)
        {
            _dbContext = dbContext;
        }

        internal async Task BeginTransaction()
        {
            _dbContextTransaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task<bool> Commit()
        {
            bool result = false;
            try
            {
                await BeginTransaction();
                 result = await CompleteTask();

                _dbContextTransaction.Commit();

                return result;
            }
            catch(SqlException ex)
            {
                await Rollback();
             
                return result;
            }

        }

        public async Task Rollback()
        {
             await _dbContextTransaction.RollbackAsync();
        }

        internal async Task<bool> CompleteTask()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
