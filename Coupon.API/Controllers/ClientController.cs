using Coupon.Application.Abstractions;
using Coupon.Application.Extension;
using Coupon.Application.InputModel.Clients;
using Coupon.Application.InputModel.Coupons;
using Coupon.Application.ViewModel.Coupon;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Coupon.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ClientController : ControllerBase, IClientController<ClientController>
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("Add-Client")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddClient([FromBody] ClientInputModel clientModel)
        {
            var client = clientModel.TOEntity();

            var id = await _clientService.InsertClient(client);

            if (!id.IsGuid())
              return BadRequest();
            
            
            return RedirectToAction(nameof(GetUser), new { id });

        }

        [HttpPatch("Client/{Id}/Deactivate")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Deactivate([FromRoute] Guid Id, [FromBody] DeactivateInputModelClient model)
        {

            if (!Id.IsGuid())
                return BadRequest("Informe o id");

            await _clientService.DeactivateClient(Id, model.reason, model.@operator);

            return Accepted();
        }

        [HttpGet("Client/{id}/Find-User")]
        [ProducesResponseType(typeof(CouponViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CouponViewModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var client = await _clientService.GetClientById(id);

            if(!id.IsGuid())
                return NotFound("Cliente não localizado");


            return Ok(client);
        }
    }
}
