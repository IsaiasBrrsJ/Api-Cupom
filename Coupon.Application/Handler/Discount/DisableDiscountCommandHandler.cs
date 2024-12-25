
using Coupon.Application.Command.Discount;
using Coupon.Application.Extension;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Discount
{
    public class DisableDiscountCommandHandler : ICommandHandler<DisableDiscountCommand>
    {
        private readonly IDiscountRepositories _discountRepositories;
        private readonly IEventRepositories<Core.Entities.Coupon.Discount> _eventStore;
        private readonly IUnitOfWork _unitOfWork;

        public DisableDiscountCommandHandler(
            IDiscountRepositories discountRepositories,
            IEventRepositories<Core.Entities.Coupon.Discount> eventStore,
            IUnitOfWork unitOfWork)
        {
            _discountRepositories = discountRepositories;
            _eventStore = eventStore;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> ExecuteAsync(DisableDiscountCommand command)
        {
            if (command is null || !command.id.IsGuid())
                return ResultViewModel.Failure($"Id with error");

            var discount = await _discountRepositories.FindByIdEntity(command.id);

            if (discount is null)
                return ResultViewModel.Failure($"{command.id} not found");


            discount.Deactivate(command.reason, command.@operator);

            _discountRepositories.Update(discount);

            await _eventStore.AddAsync(discount.eventsRead);

            await _unitOfWork.Commit();

            return ResultViewModel.Success("Updated successfully");
        }
    }
}
