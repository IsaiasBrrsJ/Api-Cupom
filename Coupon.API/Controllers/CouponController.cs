using Coupon.Application.Abstractions;
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
        private readonly ICommandBus _CommandBus;

        public CouponController(ICommandBus commandBus)
        {
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
        public async Task<IActionResult> Deactivate([FromRoute] Guid id, [FromBody] DeactivateCouponCommand command)
        {
            if (!id.IsGuid())
                return BadRequest("Informe o Id ");

            var result = await _CommandBus.Dispatcher(new DeactivateCouponCommand(id, command.@operator, command.reason));

            return Accepted();
        }

        [HttpPost("Add-Photo")]
        public async Task<IActionResult> AddPhoto([FromForm] InsertPhotoCommand command)
        {
            var result = await _CommandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return UnprocessableEntity(result);



            return Ok(result);
        }


    }
}
