using Coupon.API.Route;
using Coupon.Application.Abstractions;
using Coupon.Application.Command.Discount;
using Coupon.Application.Query.Discount;
using Coupon.Application.ViewModel.Discount;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{
    [Route(nameof(RouteApi.ROUTE))]
    [ApiController]
    public class DiscountController : ControllerBase, IDiscountController<DiscountController>
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public DiscountController(
            ICommandBus commandBus,
            IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpPost("Discount/Create-Discount")]
        [ProducesResponseType(typeof(ResultViewModel<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultViewModel),StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromBody] CreateDiscountCommand command)
        {
            var result =await _commandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return BadRequest(result);


            return Created(String.Empty, result);
        }

        [HttpGet("/Discount/{discountId}/Get-Discount")]
        [ProducesResponseType(typeof(ResultViewModel<DiscountViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCouponById([FromRoute] Guid discountId)
        {

            var result = await _queryBus.Dispatcher<GetDiscountById, ResultViewModel>(new GetDiscountById(discountId));

            if (!result.IsSuccess)
                return NotFound(result);


            return Ok(result);
        }

        [HttpPatch("/Discount/Update")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateDiscountPercentCommand command)
        {
           var result  =await _commandBus.Dispatcher(command);
            
            if(!result.IsSuccess)
                return BadRequest(result);

            return Accepted(result);
        }
    }
}
