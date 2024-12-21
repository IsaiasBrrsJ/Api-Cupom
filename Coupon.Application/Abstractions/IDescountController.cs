using Microsoft.AspNetCore.Mvc;
namespace Coupon.Application.Abstractions;

public interface IDescountController<T> where T : ControllerBase
{
}
