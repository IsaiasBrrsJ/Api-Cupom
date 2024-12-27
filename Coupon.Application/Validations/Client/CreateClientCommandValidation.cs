using Coupon.Application.Command.Client;
using FluentValidation;

namespace Coupon.Application.Validations.Client;

public sealed class CreateClientCommandValidation : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidation()
    {
        RuleFor(x => x.email)
            .NotNull().WithMessage("Incorrect email")
            .NotEmpty().WithMessage("Incorret email - empty");

        RuleFor(x => x.name)
             .NotNull().WithMessage("Incorrect name")
            .NotEmpty().WithMessage("Incorret name - empty");

        RuleFor(x => x.phoneNumber)
             .NotNull().WithMessage("Incorrect phoneNumber")
            .NotEmpty().WithMessage("Incorret phoneNumber - empty");


        RuleFor(x => x.clientType)
        .IsInEnum().WithMessage("Incorret client type, is not enum type");

        RuleFor(x => x.@operator)
        .NotEmpty().WithMessage("invalid operator")
        .MaximumLength(15).WithMessage("reason - Max Length 15");

        RuleFor(x => x.reason)
             .NotEmpty().WithMessage("invalid reason")
             .MaximumLength(100).WithMessage("reason - Max Length 100");

    }
}
