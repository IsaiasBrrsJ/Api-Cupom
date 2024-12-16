using Coupon.Core.Abstractions;
namespace Coupon.Application.Command.Coupon;

public sealed record DeactivateCouponCommand(Guid couponId, string @operator, string reason) : ICommand;
