﻿using Coupon.Application.ViewModel.Coupons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.Application.Abstractions
{
    public interface ICouponController<T> where T : ControllerBase
    {
         Task<IActionResult> AddCoupon([FromBody] CouponInputModels model, IFormFile file);
    }
}
