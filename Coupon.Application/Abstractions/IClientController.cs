using Coupon.Application.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.Application.Abstractions
{
    public interface IClientController<T> where T : ControllerBase
    {
        public Task<IActionResult> AddClient(ClientInputModel clientModel);

    }
}
