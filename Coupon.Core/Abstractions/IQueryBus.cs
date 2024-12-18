using Coupon.Core.BaseResult;

namespace Coupon.Core.Abstractions
{
    public interface IQueryBus
    {
        Task<TResult> Dispatcher<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }

}
