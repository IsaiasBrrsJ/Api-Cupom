﻿using Coupon.Application.Command.Coupon;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.Application.Abstractions
{
    public interface ICouponController<T> where T : ControllerBase
    {
        Task<IActionResult> AddCoupon(CreateCouponCommand command);
        Task<IActionResult> Disable(Guid Id, DeactivateCouponCommand command);
        Task<IActionResult> GetCouponById(Guid couponId);
        Task<IActionResult> GetAllCoupon();
        Task<IActionResult> UpdatePhoto(UpdatePhotoCommand command);

        Task<IActionResult> UpdateValidate(UpdateDateValidateCommand command);
    }
}
