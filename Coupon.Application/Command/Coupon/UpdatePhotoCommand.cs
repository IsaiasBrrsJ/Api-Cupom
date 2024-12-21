using Coupon.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Coupon.Application.Command.Coupon;
public sealed record class UpdatePhotoCommand(IFormFile photo, Guid idCoupon, string @operator, string reason) : ICommand;

