using Coupon.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Coupon.Application.Command.Coupon;
public sealed record InsertPhotoCommand(IFormFile photo, Guid couponId) : ICommand;