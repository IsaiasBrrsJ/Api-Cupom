using Coupon.Application.InputModel.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.Application.Abstractions
{
    public interface IClientController<T> where T : ControllerBase
    {
         Task<IActionResult> AddClient(ClientInputModel clientModel);
         Task<IActionResult> GetUser(Guid id);
         Task<IActionResult> Deactivate(Guid Id, DeactivateInputModelClient model);
    }
}
