namespace Coupon.Core.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task Rollback();
    }
}
