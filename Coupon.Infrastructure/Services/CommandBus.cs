using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;

namespace Coupon.Infrastructure.Services;

public class CommandBus : ICommandBus
{
    private readonly DependencyResolver _dependencyResolver;
    public CommandBus(DependencyResolver dependencyResolver)
    {
        _dependencyResolver = dependencyResolver;
    }

    public async Task<ResultViewModel> Dispatcher<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _dependencyResolver.Resolve<ICommandHandler<TCommand>>();

        var result =  await handler.ExecuteAsync(command);
         
       return result;

    }
}
