using Coupon.Core.Event;
using Coupon.Core.Externsion;

namespace Coupon.Core.Entities.Coupon
{
    public class Coupon : EntityBase
    {
        public CouponType TypeCoupon { get; init; }
        public decimal Price { get; private set; }
        public DateTime ValidAt { get; private set; }
        public bool IsExpired { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime EventDate { get; init; }
        public int MaxCoupon { get; init; }
        public DateTime CreationDate { get; private set; }
        public virtual Photo? Photo { get; set; } = default!;
        private static IList<Events<Coupon>> @event = new List<Events<Coupon>>();
        public IReadOnlyCollection<Events<Coupon>> @eventsRead = @event.AsReadOnly();


        public void Deactivate(string reason, string @operator)
        {

            InsertEvent(@operator, reason);

            IsActive = !IsActive;
        }

        public void InsertEvent(string @operator, string reason)
        {
            string checkInput = String.Empty;

            if (checkInput.IsNullOrEmptyValues(reason, @operator))
                throw new InvalidOperationException("Input model invalid");

            @event.Clear();
            @event.Add(Events<Coupon>.Factories.Create(Id, @operator, reason, nameof(Coupon)));
        }
        public void UpdatePhoto( string reason, string @operator)
        {
            InsertEvent(@operator, reason);
        }
        public void UpdatePrice(decimal price, string reason, string @operator)
        {

            if (!IsActive || price.GetType() != typeof(decimal))
                throw new InvalidOperationException("Invalid Coupon");

            InsertEvent(@operator, reason);

            Price = price;
        }

        public void UpdateValidate(DateTime validAt, string reason, string @operator)
        {

            InsertEvent(@operator, reason);
            ValidAt = validAt;
        }
        public void SetExpired(string reason, string @operator)
        {

            InsertEvent(@operator, reason);

            IsExpired = !IsExpired;
        }


        public static class Factories
        {
            public static Coupon Create(CouponType typeCoupon, decimal price, DateTime validAt, DateTime eventDate, int max)
            {
                return new Coupon
                {
                    Id = Guid.NewGuid(),
                    TypeCoupon = typeCoupon,
                    Price = price,
                    ValidAt = validAt,
                    EventDate = eventDate,
                    MaxCoupon = max,
                    CreationDate = DateTime.UtcNow,
                    IsActive = true,
                    IsExpired = false
                };

            }


        }

    }
}
