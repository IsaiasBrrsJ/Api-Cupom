using Coupon.Core.Entities.Coupon;

namespace Coupon.Application.ViewModel.Coupon
{
    public class CouponViewModel
    {

        public string BlobUrl { get; init; } = default!;
        public DateTime EventDate { get; init; } = default!;
        public string CouponType { get; init; } = default!;
        public Guid UserId { get; init; }

       
        public static class Factories
        {
             public static CouponViewModel CreateWithPhoto(string blobUrl, DateTime eventDate, string couponType, Guid userId)
            {
                return new CouponViewModel
                {
                    BlobUrl = blobUrl,
                    EventDate = eventDate,
                    CouponType = couponType,
                    UserId = userId

                };
            }

            public static CouponViewModel Create(DateTime eventDate, string couponType, Guid userId)
            {
                return new CouponViewModel
                {
                    EventDate = eventDate,
                    CouponType = couponType,
                    UserId = userId
                };
            }
        }
    }
}
