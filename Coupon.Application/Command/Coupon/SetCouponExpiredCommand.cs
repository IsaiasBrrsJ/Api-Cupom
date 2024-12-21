using Coupon.Core.Abstractions;

namespace Coupon.Application.Command.Coupon;

public sealed record SetCouponExpiredCommand(Guid id, string reason, string @operator) : ICommand;


