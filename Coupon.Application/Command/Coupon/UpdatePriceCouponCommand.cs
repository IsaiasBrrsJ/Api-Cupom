using Coupon.Core.Abstractions;

namespace Coupon.Application.Command.Coupon;

public sealed record UpdatePriceCouponCommand(Guid couponId, decimal newPrice, string reason, string @operator) : ICommand;
