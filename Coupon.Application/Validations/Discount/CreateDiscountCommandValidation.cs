﻿
using Coupon.Application.Command.Discount;
using FluentValidation;

namespace Coupon.Application.Validations.Discount;
public sealed class CreateDiscountCommandValidation : AbstractValidator<CreateDiscountCommand>
{
    public CreateDiscountCommandValidation()
    {
        RuleFor(x => x.@operator)
            .NotEmpty().WithMessage("invalid operator")
            .MaximumLength(15).WithMessage("reason - Max Length 15");

        RuleFor(x => x.reason)
             .NotEmpty().WithMessage("invalid reason")
             .MaximumLength(100).WithMessage("reason - Max Length 100");

        RuleFor(x => x.clientType)
             .IsInEnum().WithMessage("invalid ClientType ");
    }
}
