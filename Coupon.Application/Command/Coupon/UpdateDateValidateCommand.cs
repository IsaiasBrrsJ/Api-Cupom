using Coupon.Core.Abstractions;

namespace Coupon.Application.Command.Coupon;

public sealed record UpdateDateValidateCommand(Guid couponId, DateTime newDateValidate, string @operator, string reason) : ICommand;


