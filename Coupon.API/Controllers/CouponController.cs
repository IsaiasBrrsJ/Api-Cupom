﻿using Coupon.Application.Abstractions;
using Coupon.Application.Extension;
using Coupon.Application.InputModel.Coupons;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{
    [ApiController]
    [Route("API/")]
    public class CouponController : ControllerBase, ICouponController<CouponController>
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost("Adcionar-Cupom")]
        [ProducesResponseType(typeof(CouponInputModels), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddCoupon([FromForm] CouponInputModels model)
        {
            var userToEntity = model.ToEntity();

           var result = await _couponService.InsertCoupon(userToEntity, model.File);


            if(!result.IsSuccess)
                return UnprocessableEntity(result);


            return Ok(result);
        }

        [HttpPatch("Coupoun/{id}/Deactivate")]
        public async Task<IActionResult> Deactivate([FromRoute] Guid id, [FromBody] DeactivateInputModelCoupon model)
        {
            if (!id.IsGuid())
                return BadRequest("Informe o Id ");

            await _couponService.DeactivateCoupon(id, model.reason, model.@operator);
          
            return Accepted();
        }
    }
}
