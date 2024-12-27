using Coupon.Application.Command.Coupon;
using FluentValidation;

namespace Coupon.Application.Validations.Coupon;

public sealed class DeactivateCouponCommandValidation : AbstractValidator<DeactivateCouponCommand>
{
    public DeactivateCouponCommandValidation()
    {
        RuleFor(x => x.@operator)
            .NotEmpty().WithMessage("invalid operator")
            .MaximumLength(15).WithMessage("reason - Max Length 15");

        RuleFor(x => x.reason)
             .NotEmpty().WithMessage("invalid reason")
             .MaximumLength(100).WithMessage("reason - Max Length 100");
    }
}
