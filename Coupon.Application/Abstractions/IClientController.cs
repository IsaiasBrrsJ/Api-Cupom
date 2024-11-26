using Coupon.Application.ViewModel.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.Application.Abstractions
{
    public interface IClientController<T> where T : ControllerBase
    {
         Task<IActionResult> AddClient(ClientInputModel clientModel);
        Task<IActionResult> GetUser(Guid id);

    }
}
