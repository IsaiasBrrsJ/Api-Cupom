using Coupon.Core.BaseResult;

namespace Coupon.Core.Abstractions;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<ResultViewModel> ExecuteAsync(TCommand command);
}
