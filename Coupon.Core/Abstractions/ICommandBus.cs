using Coupon.Core.BaseResult;

namespace Coupon.Core.Abstractions;

public interface ICommandBus
{
    Task<ResultViewModel> Dispatcher<TCommand>(TCommand command) where TCommand : ICommand;
}
