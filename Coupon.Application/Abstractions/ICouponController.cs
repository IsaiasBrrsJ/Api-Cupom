using Coupon.Application.InputModel.Coupons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.Application.Abstractions
{
    public interface ICouponController<T> where T : ControllerBase
    {
         Task<IActionResult> AddCoupon(CouponInputModels model);
    }
}
