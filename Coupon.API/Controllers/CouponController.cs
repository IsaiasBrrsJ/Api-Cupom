using Coupon.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class CouponController : ControllerBase, ICouponController<CouponController>
    {
        [HttpPost("Adcionar-Cupom")]
        public async Task<IActionResult> AddCoupon()
        {

            return Ok();
        }
    }
}
