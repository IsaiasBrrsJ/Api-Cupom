using Coupon.Application.Command.Coupon;
using Coupon.Application.Extension;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Coupon;

public class UpdateDateValidateCommandHandler : ICommandHandler<UpdateDateValidateCommand>
{
    private readonly ICouponRepositories _couponRepositories;
    private readonly IEventRepositories<Core.Entities.Coupon.Coupon> _eventStore;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDateValidateCommandHandler(ICouponRepositories couponRepositories, IEventRepositories<Core.Entities.Coupon.Coupon> eventStore, IUnitOfWork unitOfWork)
    {
        _couponRepositories = couponRepositories;
        _eventStore = eventStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel> ExecuteAsync(UpdateDateValidateCommand command)
    {
        if (command == null || !command.couponId.IsGuid())
            return ResultViewModel.Failure("Incorrect id");

        var coupon = await _couponRepositories.FindByIdEntityAsync(command.couponId);

        if (coupon is null)
            return ResultViewModel.Failure("Coupon not found");
       

        coupon.UpdatePrice(command.newPrice, command.reason, command.@operator);

        _couponRepositories.UpdateAsync(coupon);

        await _eventStore.AddAsync(coupon.eventsRead);


        await _unitOfWork.Commit();


        return ResultViewModel.Success("Updated with succcess");

    }
}
