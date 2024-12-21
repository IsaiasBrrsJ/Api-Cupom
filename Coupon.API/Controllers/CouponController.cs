using Coupon.Application.Abstractions;
using Coupon.Application.Command.Coupon;
using Coupon.Application.Extension;
using Coupon.Application.Query.Coupon;
using Coupon.Application.ViewModel.Coupon;
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
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Deactivate([FromRoute] Guid id, [FromBody] DeactivateCouponCommand command)
        {
            if (!id.IsGuid())
                return BadRequest("Informe o Id ");

            var result = await _CommandBus.Dispatcher(new DeactivateCouponCommand(id, command.@operator, command.reason));

            return Accepted();
        }

        [HttpPost("Add-Photo")]
        [ProducesResponseType(typeof(InsertPhotoCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddPhoto([FromForm] InsertPhotoCommand command)
        {
            var result = await _CommandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return UnprocessableEntity(result);

            return Ok(result);
        }

        [HttpGet("/coupon/{couponId}/Get-Coupon")]
        [ProducesResponseType(typeof(ResultViewModel<CouponViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCouponById([FromRoute] Guid couponId)
        {

           var result = await _QueryBus.Dispatcher<GetCouponById, ResultViewModel>(new GetCouponById(couponId));

            if (!result.IsSuccess)
                return NotFound(result);


            return Ok(result);
        }

        [HttpPatch("Coupon/Update-Price")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePrice([FromBody] UpdatePriceCouponCommand command)
        {
            var result = await _CommandBus.Dispatcher(command);


            if (!result.IsSuccess)
                BadRequest(result);


           return Accepted(result);
        }

        [HttpGet("Coupon/GetAll")]
        [ProducesResponseType(typeof(IEnumerable<ResultViewModel<CouponViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCoupon()
        {
            var result = await _QueryBus.Dispatcher<GetAllCoupon, ResultViewModel>(new GetAllCoupon());

            return Ok(result);
        }

        [HttpPatch("Coupon/SetExpired-Coupon")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> SetExpiredCoupon([FromBody] SetCouponExpiredCommand command)
        {
           var result = await _CommandBus.Dispatcher(command);

            if (!result.IsSuccess)
                BadRequest(result);


            return Accepted(result);

        }

        [HttpPatch("Coupon/UpdatePhoto")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePhoto([FromForm] UpdatePhotoCommand command)
        {
            var result = await _CommandBus.Dispatcher(command);


            if (!result.IsSuccess)
                return BadRequest(result);

            return Accepted(result);
        }
    }
}
