
using Coupon.Application.Query.Coupon;
using Coupon.Application.ViewModel.Coupon;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Coupon;

public class GetAllCouponQueryHandler : IQueryHanlder<GetAllCoupon, ResultViewModel>
{
    private readonly ICouponRepositories _couponRepositories;
    public GetAllCouponQueryHandler(ICouponRepositories couponRepositories)
    {
        _couponRepositories = couponRepositories;
    }

    public async Task<ResultViewModel> Handler(GetAllCoupon query)
    {
        var result = await _couponRepositories.GetAllAsync();



        if (!result.Any())
            return ResultViewModel<IEnumerable<CouponViewModel>>.Success(Enumerable.Empty<CouponViewModel>(), "No data");


        IEnumerable<CouponViewModel> data = result
        .Select(item => CouponViewModel
        .Factories
        .CreateWithPhoto(item.Photo!.BlobUrl, item.EventDate, Enum.GetName(typeof(CouponType), item.TypeCoupon)!, item.Id))
        .ToList();

        return ResultViewModel<IEnumerable<CouponViewModel>>.Success(data, "Success");

    }
}

