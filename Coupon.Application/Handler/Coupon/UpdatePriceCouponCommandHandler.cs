using Coupon.Application.Command.Coupon;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace Coupon.Application.Handler.Coupon;

public class UpdatePriceCouponCommandHandler : ICommandHandler<UpdatePriceCouponCommand>
{

    private readonly ICouponRepositories _couponRepositories;
    private readonly IEventRepositories<Core.Entities.Coupon.Coupon> _eventStore;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdatePriceCouponCommandHandler(
        ICouponRepositories couponRepositories, 
        IEventRepositories<Core.Entities.Coupon.Coupon> eventStore, 
        IUnitOfWork unitOfWork, 
        IHttpContextAccessor contextAccessor)
    {
        _couponRepositories = couponRepositories;
        _eventStore = eventStore;
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<ResultViewModel> ExecuteAsync(UpdatePriceCouponCommand command)
    {
        var coupon = await _couponRepositories.FindByIdEntityAsync(command.couponId);

        if (coupon == null)
            return ResultViewModel.Failure("Not Found");

        var teste = _contextAccessor.HttpContext.Items;

        coupon.UpdatePrice(command.newPrice, command.reason, command.@operator);

        await _eventStore.AddAsync(coupon.eventsRead);
        _couponRepositories.UpdateAsync(coupon);

        await _unitOfWork.Commit();

        return ResultViewModel.Success("Updated Accepted");

    }
}
