using Coupon.Application.Command.Coupon;
using FluentValidation;

namespace Coupon.Application.Validations.Coupon;

public sealed class InsertPhotoCommandValidation : AbstractValidator<InsertPhotoCommand>
{
    public InsertPhotoCommandValidation()
    {
        RuleFor(x => x.photo)
            .NotNull().WithMessage("Invalid file form photo");
    }
}
