
using Coupon.Application.Command.Coupon;
using FluentValidation;

namespace Coupon.Application.Validations.Coupon;

public sealed class UpdateDateValidateCommandValidation : AbstractValidator<UpdateDateValidateCommand>
{
    public UpdateDateValidateCommandValidation()
    {
        RuleFor(x => x.@operator)
      .NotEmpty().WithMessage("invalid operator")
      .MaximumLength(15).WithMessage("reason - Max Length 15");

        RuleFor(x => x.reason)
             .NotEmpty().WithMessage("invalid reason")
             .MaximumLength(100).WithMessage("reason - Max Length 100");

        RuleFor(x => x.newDateValidate)
            .NotNull().WithMessage("Invalid date validate");
    }
}
