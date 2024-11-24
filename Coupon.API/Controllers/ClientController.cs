using Coupon.Application.Abstractions;
using Coupon.Application.Command;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase, IClientController<ClientController>
    {
        [HttpPost("Adcionar-Cliente")]
        public async Task<IActionResult> AddClient([FromBody] ClientInputModel clientModel)
        {
            var listFruits = new[] { "Bana", "Abacate", "Abobora", "Melancia", "Ave" };



            return Ok();
        }

       
    }
}
