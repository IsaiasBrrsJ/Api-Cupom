using Coupon.Application.Command.Coupon;
using FluentValidation;

namespace Coupon.Application.Validations.Coupon;

public sealed class UpdatePhotoCommandValidation : AbstractValidator<UpdatePhotoCommand>
{
    public UpdatePhotoCommandValidation()
    {
        RuleFor(x => x.photo)
           .NotNull().WithMessage("Invalid file form photo");

        RuleFor(x => x.@operator)
          .NotEmpty().WithMessage("invalid operator")
          .MaximumLength(15).WithMessage("reason - Max Length 15");

        RuleFor(x => x.reason)
             .NotEmpty().WithMessage("invalid reason")
             .MaximumLength(100).WithMessage("reason - Max Length 100");
    }
}
