using Coupon.Application.Command.Coupon;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Coupon;

public sealed record SetCouponExpiredCommandHandler : ICommandHandler<SetCouponExpiredCommand>
{
    private readonly ICouponRepositories _couponRepositories;
    private readonly IEventRepositories<Core.Entities.Coupon.Coupon> _eventStore;
    private readonly IUnitOfWork _unitOfWork;
    public SetCouponExpiredCommandHandler(
        ICouponRepositories couponRepositories, 
        IEventRepositories<Core.Entities.Coupon.Coupon> eventStore, 
        IUnitOfWork unitOfWork)
    {
        _couponRepositories = couponRepositories;
        _eventStore = eventStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel> ExecuteAsync(SetCouponExpiredCommand command)
    {
        var result = await _couponRepositories.FindByIdEntityAsync(command.id);

        if (result == null)
            return ResultViewModel.Failure($"{command.id} | Not Found");


        result.SetExpired(command.reason, command.@operator);

        await _eventStore.AddAsync(result.eventsRead);
        _couponRepositories.UpdateAsync(result);

        await _unitOfWork.Commit();


        return ResultViewModel.Success("Accepted");
    }
}
