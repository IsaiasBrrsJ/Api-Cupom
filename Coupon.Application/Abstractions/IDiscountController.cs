
using Coupon.Application.Command.Discount;
using Microsoft.AspNetCore.Mvc;
namespace Coupon.Application.Abstractions;

public interface IDiscountController<T> where T : ControllerBase
{
     Task<IActionResult> Create(CreateDiscountCommand command);
     Task<IActionResult> Update(UpdateDiscountPercentCommand command);
     Task<IActionResult> GetAll();
    Task<IActionResult> GetById(Guid id);
    Task<IActionResult> Disable(DisableDiscountCommand command);

    Task<IActionResult> Active(ActiveDiscountCommand command);
}
