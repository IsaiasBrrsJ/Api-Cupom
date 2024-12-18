using Coupon.Application.Abstractions;
using Coupon.Application.Command.Coupon;
using Coupon.Application.Extension;
using Coupon.Application.Query.Coupon;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{
    [ApiController]
    [Route("API/")]
    public class CouponController : ControllerBase, ICouponController<CouponController>
    {
        private readonly ICommandBus _CommandBus;
        private readonly IQueryBus _QueryBus;
        public CouponController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _CommandBus = commandBus;
            _QueryBus = queryBus;
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

        [HttpGet("/coupon/{couponId}/Get-Coupon")]
        public async Task<IActionResult> GetCouponById([FromRoute] Guid couponId)
        {

           var result = await _QueryBus.Dispatcher<GetCouponById, ResultViewModel>(new GetCouponById(couponId));

            if (!result.IsSuccess)
                return NotFound("User não encontrado");


            return Ok(result);
        }

    }
}
