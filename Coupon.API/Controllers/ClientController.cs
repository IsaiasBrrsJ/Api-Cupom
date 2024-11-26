using Coupon.Application.Abstractions;
using Coupon.Application.ViewModel.Clients;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Coupon.API.Controllers
{
    [Route("api/[controller]")]
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

            if (id is Guid guidId)
                return RedirectToAction(nameof(GetUser), new { id });


            return BadRequest();
        }

        [HttpGet("Client/{id}/Find-User")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {

            return Ok();
        }
    }
}
