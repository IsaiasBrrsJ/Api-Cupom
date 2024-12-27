
using Coupon.Application.Command.Discount;
using FluentValidation;

namespace Coupon.Application.Validations.Discount;

public class CreateDiscountCommandValidation : AbstractValidator<CreateDiscountCommand>
{
    public CreateDiscountCommandValidation()
    {
        RuleFor(x => x.@operator)
            .NotEmpty().WithMessage("invalid operator");
    }
}
