using Coupon.Application.Abstractions;
using Coupon.Application.ViewModel.Coupons;
using Coupon.Core.Repositories;
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
        public async Task<IActionResult> AddCoupon([FromBody] CouponInputModels model, IFormFile? file)
        {
            var userToEntity = model.ToEntity();

           var guidUser = await _couponService.InsertCoupon(userToEntity);
        
            
             


        }
    }
}
