
using Coupon.Application.Abstractions;
using Coupon.Application.Command.Client;
using Coupon.Application.Query.Client;
using Coupon.Application.ViewModel.Client;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Microsoft.AspNetCore.Mvc;


namespace Coupon.API.Controllers
{

    [Route("api/")]
    [ApiController]
    public class ClientController : ControllerBase, IClientController<ClientController>
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        public ClientController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpPost("Add")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromBody]  CreateClientCommand command)
        {
            var result = await _commandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return UnprocessableEntity(result);

            return Created("", result);

        }
        [HttpPatch("Disable")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Disable([FromBody] DisableClientCommand command)
        {
            var result = await _commandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return UnprocessableEntity(result);

            return Accepted(result);


        }
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<ClientViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _queryBus.Dispatcher<GetAllClients, ResultViewModel> (new GetAllClients());

            return Ok(result);

        }

        [HttpGet("Client/{id}/Find-User")]
        [ProducesResponseType(typeof(ResultViewModel<ClientViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultViewModel<ClientViewModel>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBydId([FromRoute] Guid id)
        {
            var result = await _queryBus.Dispatcher<GetByIdClient, ResultViewModel>(new GetByIdClient(id));

            if (!result.IsSuccess)
                return NotFound(result);


            return Ok(result);
        }

        [HttpPatch("Update-Email")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateEmail(UpdateEmailCommand command)
        {
            var result = await _commandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return UnprocessableEntity(result);

            return Accepted(result);
        }

        [HttpPatch("Update-Name")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateName(UpdateNameCommand command)
        {
            var result = await _commandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return UnprocessableEntity(result);

            return Accepted(result);
        }

        [HttpPatch("Update-PhoneNumber")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdatePhoneNumber(UpdatePhoneNumberCommand command)
        {
            var result = await _commandBus.Dispatcher(command);

            if (!result.IsSuccess)
                return UnprocessableEntity(result);

            return Accepted(result);
        }
    }
}
