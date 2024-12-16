using Coupon.Core.Event;

namespace Coupon.Core.Entities.Coupon
{
    public class Photo : EntityBase
    {
        public string FileName { get; init ; } = default!;
        public DateTime AddedOn { get; init; }
        public string BlobUrl { get; init; } = default!;
        public string ContentType { get; init; } = default!;
        public virtual Coupon Coupon { get; init; } = default!;
        public Guid CouponId { get; init; }
        private static IList<Events<Photo>> @event = new List<Events<Photo>>();
        public IReadOnlyCollection<Events<Photo>> @eventsRead = @event.AsReadOnly();

        public void InsertEvent(string @operator, string reason)
        {
            @event.Add(Events<Photo>.Factories.Create(Id, @operator, reason, nameof(Coupon)));
        }
        public static class Factories
        {
            public static Photo Create(string fileName,string blolUrl, string contetType ,Guid IdCoupon)
            {
                return new Photo
                {
                    FileName = fileName,
                    BlobUrl = blolUrl,
                    ContentType = contetType,
                    AddedOn = DateTime.UtcNow,
                    CouponId = IdCoupon
                };
            }
        }
    }
}
