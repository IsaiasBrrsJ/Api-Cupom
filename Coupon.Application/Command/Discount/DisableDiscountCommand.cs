using Coupon.Core.Abstractions;

namespace Coupon.Application.Command.Discount
{
    public sealed record DisableDiscountCommand(Guid id, string reason, string @operator) : ICommand;
   
}
