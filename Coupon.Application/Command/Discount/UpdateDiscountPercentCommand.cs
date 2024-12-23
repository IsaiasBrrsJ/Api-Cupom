
using Coupon.Core.Abstractions;

namespace Coupon.Application.Command.Discount;

public sealed record UpdateDiscountPercentCommand(Guid discountId, decimal percent, string @operator, string reason) : ICommand;
