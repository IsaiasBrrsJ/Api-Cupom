using Coupon.Application.Query.Coupon;
using Coupon.Application.ViewModel.Coupon;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;

namespace Coupon.Application.Handler.Coupon;

public class GetCoupounByIdQueryHandler : IQueryHanlder<GetCouponById, ResultViewModel>
{
    private readonly ICouponRepositories _couponRespositories;
    public GetCoupounByIdQueryHandler(ICouponRepositories couponRespositories)
    {
        _couponRespositories = couponRespositories;
    }

    public async Task<ResultViewModel> Handler(GetCouponById query)
    {
      var result =    await _couponRespositories.GetByIdAsync(query.couponId);


        if (result is not Core.Entities.Coupon.Coupon)
            return ResultViewModel.Failure("Not found");

        var couponViewModel = CouponViewModel.Factories.CreateWithPhoto(result.Photo!.BlobUrl, result.EventDate, Enum.GetName(typeof(CouponType), result.TypeCoupon)!, result.Id);

        return ResultViewModel<CouponViewModel>.Success(couponViewModel, "Success");
    }
}
