using Coupon.Application.Abstractions;
using Coupon.Application.InputModel.Coupons;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class CouponController : ControllerBase, ICouponController<CouponController>
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpPost("Adcionar-Cupom")]
        public async Task<IActionResult> AddCoupon( CouponInputModels model)
        {
            var userToEntity = model.ToEntity();

           var guidUser = await _couponService.InsertCoupon(userToEntity, model.File);

        
            return Ok();
        }
        
    }
}
