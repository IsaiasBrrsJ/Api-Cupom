﻿using Coupon.Application.Abstractions;
using Coupon.Application.Command.Coupon;
using Coupon.Application.Extension;
using Coupon.Application.InputModel.Coupons;
using Coupon.Core.Abstractions;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{
    [ApiController]
    [Route("API/")]
    public class CouponController : ControllerBase, ICouponController<CouponController>
    {
        private readonly ICouponService _couponService;
        private readonly ICommandBus _CommandBus;

        public CouponController(ICouponService couponService, ICommandBus commandBus)
        {
            _couponService = couponService;
            _CommandBus = commandBus;
        }

        [HttpPost("Adcionar-Cupom")]
        [ProducesResponseType(typeof(CreateCouponCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddCoupon([FromForm] CreateCouponCommand command)
        {

            var result = await _CommandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return UnprocessableEntity();


            return Created("", result);
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
