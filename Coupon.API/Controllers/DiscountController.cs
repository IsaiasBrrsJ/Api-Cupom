using Coupon.Application.Abstractions;
using Coupon.Application.Command.Discount;
using Coupon.Application.Query.Discount;
using Coupon.Application.ViewModel.Discount;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{
    [Route("api/")]
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

        [HttpPatch("Discount/Active")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Active([FromBody] ActiveDiscountCommand command)
        {
            var result = await _commandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return UnprocessableEntity(result);


            return Accepted(result);
        }

        [HttpPost("Discount/Create")]
        [ProducesResponseType(typeof(ResultViewModel<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultViewModel),StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromBody] CreateDiscountCommand command)
        {
            var result =await _commandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return UnprocessableEntity(result);


            return Created(String.Empty, result);
        }
        [HttpPatch("Discount/Disable")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Disable(DisableDiscountCommand command)
        {
            var result = await _commandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return UnprocessableEntity(result);


            return Accepted(result);
        }

        [HttpGet("Discount/GetAll")]
        [ProducesResponseType(typeof(ResultViewModel<IEnumerable<DiscountViewModel>>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetAll()
        {
            var result = await _queryBus.Dispatcher<GetAllDiscount, ResultViewModel>(new GetAllDiscount());

            return Ok(result);
        }

        [HttpGet("Discount/{id}/GetById")]
        [ProducesResponseType(typeof(ResultViewModel<DiscountViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _queryBus.Dispatcher<GetDiscountById, ResultViewModel>(new GetDiscountById(id));

            if (!result.IsSuccess)
                return NotFound(result);


            return Ok(result);  
        }

        [HttpPatch("Discount/Update")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update([FromBody] UpdateDiscountPercentCommand command)
        {
           var result  =await _commandBus.Dispatcher(command);
            
            if(!result.IsSuccess)
                return UnprocessableEntity(result);

            return Accepted(result);
        }
    }
}
