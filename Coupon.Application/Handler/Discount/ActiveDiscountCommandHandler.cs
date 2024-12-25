using Coupon.Application.Command.Discount;
using Coupon.Application.Extension;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Discount;

public class ActiveDiscountCommandHandler : ICommandHandler<ActiveDiscountCommand>
{
    private readonly IDiscountRepositories _discountRepositories;
    private readonly IEventRepositories<Core.Entities.Coupon.Discount> _eventStore;
    private readonly IUnitOfWork _unitOfWork;
    public ActiveDiscountCommandHandler(
        IDiscountRepositories discountRepositories, 
        IEventRepositories<Core.Entities.Coupon.Discount> eventStore, 
        IUnitOfWork unitOfWork)
    {
        _discountRepositories = discountRepositories;
        _eventStore = eventStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel> ExecuteAsync(ActiveDiscountCommand command)
    {
        if (command is null || !command.idDiscount.IsGuid())
            return ResultViewModel.Failure($"Id with error");

        var discount = await _discountRepositories.FindByIdEntity(command.idDiscount);

        if (discount is null)
            return ResultViewModel.Failure($"{command.idDiscount} not found");


        discount.Activate(command.reason, command.@operator);

        _discountRepositories.Update(discount);

        await _eventStore.AddAsync(discount.eventsRead);

        await _unitOfWork.Commit();

        return ResultViewModel.Success("Updated successfully");
    }
}
