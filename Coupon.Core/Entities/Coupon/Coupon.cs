using Coupon.Core.Entities.Client;
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
        public virtual Photo? Photo { get; private set; } = default!;
        public Guid? PhotoId { get; private set; }
        private static IList<Events<Coupon>> @event = new List<Events<Coupon>>(); 
        public IReadOnlyCollection<Events<Coupon>> @eventsRead = @event.AsReadOnly(); 
        
        public void Deactivate(string reason, string @operator)
        {
            string checkInput = String.Empty;

            if (checkInput.IsNullOrEmptyValues(reason, @operator))
                throw new InvalidOperationException("Input model invalid");
            
            IsActive = !IsActive;
            @event.Add(Events<Coupon>.Factories.Create(Id, @operator, reason, nameof(Coupon)));
        }

        public void UpdatePhoto(Photo photo)
        {
            Photo = photo;
        }
        public void UpdatePrice(decimal price)
        {

            if (!IsActive)
                throw new InvalidOperationException("Cupom já desativado");


            Price = price;

        }

        public void UpdateValidate(DateTime validAt)
        {

            ValidAt = validAt;
        }
        public void SetExpired()
        {
           

            IsExpired = !IsExpired;
        }

      
        public static class Factories
        {
            public static Coupon Create(CouponType typeCoupon, decimal price, DateTime validAt, DateTime eventDate, int max)
            {
                return new Coupon
                {
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
