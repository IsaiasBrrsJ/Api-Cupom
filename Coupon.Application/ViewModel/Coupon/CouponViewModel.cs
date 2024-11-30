using Coupon.Core.Entities.Coupon;

namespace Coupon.Application.ViewModel.Coupon
{
    public class CouponViewModel
    {

        public string BlobUrl { get;  set; } = default!;
        public DateTime EventDate { get; set; } = default!;
        public string CouponType { get; set; } 
        public Guid UserId { get; set; }

       
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
