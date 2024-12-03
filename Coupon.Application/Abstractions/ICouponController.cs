using Coupon.Application.InputModel.Coupons;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.Application.Abstractions
{
    public interface ICouponController<T> where T : ControllerBase
    {
        Task<IActionResult> AddCoupon(CouponInputModels model);
        Task<IActionResult> Deactivate(Guid Id, DeactivateInputModelCoupon model);
    }
}
