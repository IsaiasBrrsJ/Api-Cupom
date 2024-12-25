
using Coupon.Application.Extension;
using Coupon.Application.ViewModel.Client;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{

    [Route("api/")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        //[HttpPost("Add-Client")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> AddClient([FromBody] ClientInputModel clientModel)
        //{
        //    var client = clientModel.TOEntity();

        //    var id = await _clientService.InsertClient(client);

        //    if (!id.IsGuid())
        //      return BadRequest();
            
            
        //    return RedirectToAction(nameof(GetUser), new { id });

        //}

        //[HttpPatch("Client/{Id}/Deactivate")]
        //[ProducesResponseType((int)HttpStatusCode.Accepted)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> Deactivate([FromRoute] Guid Id, [FromBody] DeactivateInputModelClient model)
        //{

        //    if (!Id.IsGuid())
        //        return BadRequest("Informe o id");

        //    await _clientService.DeactivateClient(Id, model.reason, model.@operator);

        //    return Accepted();
        //}

        [HttpGet("Client/{id}/Find-User")]
        [ProducesResponseType(typeof(ClientViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientViewModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var client = await _clientService.GetClientById(id);

            if(!id.IsGuid())
                return NotFound("Cliente não localizado");

            var clietViewModel = ClientViewModel.Create(client.Name, client.PhoneNumber, client.ClientType, client.IsActive);

            return Ok(clietViewModel);
        }

        //[HttpPatch("Client/{id}/Update-Email")]
        //[ProducesResponseType(typeof(UpdateEmail), StatusCodes.Status202Accepted)]
        //public async Task<IActionResult> UpdateEmail([FromRoute] Guid id, [FromBody] UpdateEmail model)
        //{
        //   await _clientService.UpdateEmail(id, model.email, model.reason, model.@operator);

        //    return Accepted();
        //}

        //[HttpPatch("Client/{id}/Update-Name")]
        //[ProducesResponseType(typeof(UpdateName), StatusCodes.Status202Accepted)]
        //public async Task<IActionResult> UpdateName([FromRoute] Guid id, [FromBody] UpdateName model)
        //{
        //    await _clientService.UpdateName(id, model.name, model.reason, model.@operator);

        //    return Accepted("Requisição aceita");
        //}

        //[HttpPatch("Client/{id}/Update-PhoneNumber")]
        //[ProducesResponseType(typeof(UpdatePhoneNumber), StatusCodes.Status202Accepted)]
        //public async Task<IActionResult> UpdatePhoneNumber([FromRoute] Guid id, [FromBody] UpdatePhoneNumber model)
        //{
        //    await _clientService.UpdatePhoneNumber(id, model.phoneNumber, model.reason, model.@operator);

        //    return Accepted("Requisição aceita");
        //}
    }
}
