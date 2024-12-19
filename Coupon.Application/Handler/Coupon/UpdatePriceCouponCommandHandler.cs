using Coupon.Application.Command.Coupon;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Coupon;

public class UpdatePriceCouponCommandHandler : ICommandHandler<UpdatePriceCouponCommand>
{

    private readonly ICouponRepositories _couponRepositories;
    private readonly IEventRepositories<Core.Entities.Coupon.Coupon> _eventStore;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePriceCouponCommandHandler(ICouponRepositories couponRepositories, IEventRepositories<Core.Entities.Coupon.Coupon> eventStore, IUnitOfWork unitOfWork)
    {
        _couponRepositories = couponRepositories;
        _eventStore = eventStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel> ExecuteAsync(UpdatePriceCouponCommand command)
    {
        var coupon = await _couponRepositories.FindByIdEntityAsync(command.couponId);

        if (coupon == null)
            return ResultViewModel.Failure("Not Found");

        coupon.UpdatePrice(command.newPrice, command.reason, command.@operator);

        await _eventStore.AddAsync(coupon.eventsRead);
        _couponRepositories.UpdateAsync(coupon);

        await _unitOfWork.Commit();

        return ResultViewModel.Success("Updated Accepted");

    }
}
