
using Coupon.Application.Command.Discount;
using Microsoft.AspNetCore.Mvc;
namespace Coupon.Application.Abstractions;

public interface IDiscountController<T> where T : ControllerBase
{
     Task<IActionResult> Create(CreateDiscountCommand command);
     Task<IActionResult> Update(UpdateDiscountPercentCommand command);
}
