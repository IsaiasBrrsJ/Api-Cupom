using Coupon.Application.Command.Client;
using FluentValidation;

namespace Coupon.Application.Validations.Client;

public sealed class UpdateNameCommandValidation : AbstractValidator<UpdateNameCommand>
{
    public UpdateNameCommandValidation()
    {
        RuleFor(x => x.@operator)
             .NotEmpty().WithMessage("invalid operator")
            .MaximumLength(15).WithMessage("reason - Max Length 15");

        RuleFor(x => x.reason)
             .NotEmpty().WithMessage("invalid reason")
             .MaximumLength(100).WithMessage("reason - Max Length 100");

        RuleFor(x => x.newName)
             .NotNull().WithMessage("Incorrect newName")
             .NotEmpty().WithMessage("Incorret newName - empty");
    }
}
