using Coupon.Core.Abstractions;

namespace Coupon.Infrastructure.Services;

public class QueryBus : IQueryBus
{
    private readonly DependencyResolver _dependencyResolver;
    public QueryBus(DependencyResolver dependencyResolver)
    {
        _dependencyResolver = dependencyResolver;
    }

    public async Task<TResult> Dispatcher<TQuery, TResult>(TQuery query) where TQuery : IQuery
    {
        var handler = _dependencyResolver.Resolve<IQueryHanlder<TQuery, TResult>>();

        var result = await handler.Handler(query);

        return result;
    }
}
