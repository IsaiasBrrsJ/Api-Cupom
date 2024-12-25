
using Coupon.Application.Query.Discount;
using Coupon.Application.ViewModel.Discount;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Client;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Discount;

public class GetAllDiscountQueryHandler : IQueryHanlder<GetAllDiscount, ResultViewModel>
{
    private readonly IDiscountRepositories _discountRepository;
    public GetAllDiscountQueryHandler(IDiscountRepositories discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<ResultViewModel> Handler(GetAllDiscount query)
    {
        var result = await _discountRepository.GetAll();

        if (!result.Any())
            return ResultViewModel<IEnumerable<DiscountViewModel>>.Success(Enumerable.Empty<DiscountViewModel>(), "Empty");

        var resultviewModel = result.Select(x => new DiscountViewModel(Enum.GetName(typeof(ClientType), x.TipoDesconto)!, x.PercentDescount, x.CreateAt, x.IsActive)).ToList();    


        return ResultViewModel<IEnumerable<DiscountViewModel>>.Success(resultviewModel, "Success");
    }
}
