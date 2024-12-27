using Coupon.Application.Command.Coupon;
using FluentValidation;

namespace Coupon.Application.Validations.Coupon;

public sealed class CreateCouponCommandValidation : AbstractValidator<CreateCouponCommand>
{
    public CreateCouponCommandValidation()
    {
        RuleFor(x => x.EventDate)
            .NotNull().WithMessage("Invalid event date");

        RuleFor(x => x.Price)
                .NotNull().WithMessage("Invalid price");

        RuleFor(x => x.MaxCoupon)
                .NotNull().WithMessage("Invalid MaxCoupon");

        RuleFor(x => x.CouponType)
            .IsInEnum().WithMessage("Coupon type is not valid enum type");

         RuleFor(x => x.ValidAt)
                .NotNull().WithMessage("Invalid ValidAt");
    }
}
