using Coupon.Core.Entities.Coupon;
using Microsoft.AspNetCore.Http;

namespace Coupon.Application.ViewModel.Coupons
{
    public class CouponInputModels
    {

        public decimal Price { get;  set; }
        public DateTime ValidAt { get;  set; }
        public DateTime EventDate { get; init; }
        public int MaxCoupon { get; init; }
        public CouponType CouponType { get; set; } = default!;


        public Core.Entities.Coupon.Coupon ToEntity()
        {
             return Core
                .Entities
                .Coupon
                .Coupon
                .Factories
                .Create(CouponType, Price, ValidAt, EventDate, MaxCoupon);
        }
    }
}