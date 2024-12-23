using Coupon.Application.Query.Discount;
using Coupon.Application.ViewModel.Discount;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Client;
using Coupon.Core.Repositories;


namespace Coupon.Application.Handler.Discount;
public class GetDiscountByIdQueryHandler : IQueryHanlder<GetDiscountById, ResultViewModel>
{
    private readonly IDiscountRepositories _discountRepositories;

    public GetDiscountByIdQueryHandler(
        IDiscountRepositories discountRepositories)
    {
        _discountRepositories = discountRepositories;
    }

    public async Task<ResultViewModel> Handler(GetDiscountById query)
    {
        var result = await _discountRepositories.GetByIdDapper(query.descountId);

        if (result == null)
            return ResultViewModel.Failure($"{query.descountId} Not Found");


        var discountViewModel = new DiscountViewModel(Enum.GetName(typeof(ClientType), result.TipoDesconto)!, result.PercentDescount, result.CreateAt, result.IsActive);

        return ResultViewModel<DiscountViewModel>.Success(discountViewModel, String.Empty);
    }
}
