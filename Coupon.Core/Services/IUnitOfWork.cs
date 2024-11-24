namespace Coupon.Core.Services
{
    public interface IUnitOfWork
    {
         Task<bool> Commit();

        Task Rollback();
    }
}
