using Coupon.Application.Command.Discount;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Discount;

public class CreateDiscountCommandHandler : ICommandHandler<CreateDiscountCommand>
{
    private readonly IDiscountRepositories _descountRepositories;
    private readonly IEventRepositories<Core.Entities.Coupon.Discount> _eventStore;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiscountCommandHandler(
        IDiscountRepositories descountRepositories,
        IEventRepositories<Core.Entities.Coupon.Discount> eventStore,
        IUnitOfWork unitOfWork)
    {
        _descountRepositories = descountRepositories;
        _eventStore = eventStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel> ExecuteAsync(CreateDiscountCommand command)
    {
        if (command == null)
            return ResultViewModel.Failure("Incorrect model");


        var descount = Core.Entities.Coupon.Discount.Factories.Create(command.percent, command.clientType);

        descount.InsertEvent(command.@operator, command.reason);

       var result = await _descountRepositories.Create(descount);

        await _eventStore.AddAsync(descount.eventsRead);

        await _unitOfWork.Commit();

        return ResultViewModel<Guid>.Success(result, $"Created with success");

    }
}

