using Coupon.Application.Command.Client;
using FluentValidation;

namespace Coupon.Application.Validations.Client;

public sealed class UpdateEmailCommandValidation : AbstractValidator<UpdateEmailCommand>
{
    public UpdateEmailCommandValidation()
    {
        RuleFor(x => x.@operator)
       .NotEmpty().WithMessage("invalid operator")
       .MaximumLength(15).WithMessage("reason - Max Length 15");

        RuleFor(x => x.reason)
             .NotEmpty().WithMessage("invalid reason")
             .MaximumLength(100).WithMessage("reason - Max Length 100");

        RuleFor(x => x.newEmail)
        .NotNull().WithMessage("Incorrect email")
        .NotEmpty().WithMessage("Incorret email - empty");
    }
}
