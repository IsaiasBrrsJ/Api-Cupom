using Coupon.Core.Abstractions;

namespace Coupon.Application.Query.Coupon;

public sealed  record GetCouponById(Guid couponId) : IQuery;


