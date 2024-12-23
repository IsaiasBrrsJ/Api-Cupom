using Coupon.Core.Abstractions;

namespace Coupon.Application.Command.Coupon;

public sealed record UpdateDateValidateCommand(Guid couponId, decimal newPrice, string @operator, string reason) : ICommand;


