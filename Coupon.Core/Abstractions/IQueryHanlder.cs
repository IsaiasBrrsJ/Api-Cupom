using Coupon.Core.BaseResult;

namespace Coupon.Core.Abstractions;

public interface IQueryHanlder<in TQuery, TResult> where TQuery : IQuery
{
    Task<TResult> Handler(TQuery query);
}

