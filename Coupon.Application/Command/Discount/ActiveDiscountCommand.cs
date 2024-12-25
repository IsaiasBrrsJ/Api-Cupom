using Coupon.Core.Abstractions;

namespace Coupon.Application.Command.Discount;
public sealed record ActiveDiscountCommand(Guid idDiscount, string reason, string @operator) : ICommand;
