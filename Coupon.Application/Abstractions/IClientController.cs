
using Coupon.Application.Command.Client;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.Application.Abstractions
{
    public interface IClientController<T> where T : ControllerBase
    {
        Task<IActionResult> Create(CreateClientCommand command);
        Task<IActionResult> GetBydId(Guid id);
        Task<IActionResult> Disable(DisableClientCommand command);
        Task<IActionResult> UpdateName(UpdateNameCommand command);
        Task<IActionResult> UpdateEmail(UpdateEmailCommand command);
        Task<IActionResult> UpdatePhoneNumber(UpdatePhoneNumberCommand command);
        Task<IActionResult> GetAll();
    }
}
