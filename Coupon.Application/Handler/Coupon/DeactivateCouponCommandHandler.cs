using Azure;
using Coupon.Application.Command.Coupon;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;
using Coupon.Core.Services;

namespace Coupon.Application.Handler.Coupon;
public class DeactivateCouponCommandHandler : ICommandHandler<DeactivateCouponCommand>
{
    private readonly ICouponRepositories _couponRepositories;
    private readonly IEventRepositories<Core.Entities.Coupon.Coupon> _eventStore;
    private readonly IUnitOfWork _unitOfWork;
  
    public DeactivateCouponCommandHandler(
      ICouponRepositories couponRepositories,
      IEventRepositories<Core.Entities.Coupon.Coupon> eventStore,
      IUnitOfWork unitOfWork)
    {
        _couponRepositories = couponRepositories;
        _eventStore = eventStore;
        _unitOfWork = unitOfWork;
   
    }
    public async Task<ResultViewModel> ExecuteAsync(DeactivateCouponCommand command)
    {
        var client = await _couponRepositories.GetByIdAsync(command.couponId);

        client.Deactivate(command.reason, command.@operator);

        await _eventStore.AddAsync(client.eventsRead);

        await _unitOfWork.Commit();


        return ResultViewModel.Success("User deactivate with success");
    }
}
