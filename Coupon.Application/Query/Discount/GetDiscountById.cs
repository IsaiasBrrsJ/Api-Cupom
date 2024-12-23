
using Coupon.Core.Abstractions;

namespace Coupon.Application.Query.Discount;

public sealed record GetDiscountById(Guid descountId) : IQuery;
    