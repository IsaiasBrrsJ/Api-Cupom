using Coupon.Core.Entities.Client;
using Coupon.Core.Event;
using Coupon.Core.Externsion;

namespace Coupon.Core.Entities.Coupon
{
    public class Discount : EntityBase
    {
        public ClientType TipoDesconto { get; init; }
        public decimal PercentDescount { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime? UpdateAt { get; private set; }

        private static IList<Events<Discount>> @event = new List<Events<Discount>>();
        public IReadOnlyCollection<Events<Discount>> @eventsRead = @event.AsReadOnly();
        public void UpdateDiscount(decimal percentDescount, string reason, string @operator)
        {
            if (percentDescount < 0.01m || percentDescount > 0.15m)
                throw new InvalidOperationException("Invalid discount percentage");

            InsertEvent(@operator, reason);

            PercentDescount = percentDescount;

            UpdateAt = DateTime.UtcNow;
        }
        public void Activate(string reason, string @operator)
        {
            if(IsActive)
                throw new InvalidOperationException("Coupon is active");


            InsertEvent(@operator, reason);

            IsActive = !IsActive;

        }
        public void Deactivate(string reason, string @operator)
        {
            if(!IsActive)
                throw new InvalidOperationException("Coupon is disable");

            InsertEvent(@operator, reason);

            IsActive = !IsActive;
        }
        public void InsertEvent(string @operator, string reason)
        {

            string checkInput = String.Empty;

            if (checkInput.IsNullOrEmptyValues(reason, @operator))
                throw new InvalidOperationException("Invalid Input model");

            @event.Clear();

            @event.Add(Events<Discount>.Factories.Create(Id, @operator, reason, nameof(Discount)));

        }
        public static class Factories
        {
            public static Discount Create(decimal percent, ClientType client)
            {
                if ((percent < 0.0m || percent > 0.15m) || !Enum.IsDefined(typeof(ClientType), client))
                    throw new InvalidOperationException("Invalid model");

                return new Discount
                {
                    Id = Guid.NewGuid(),
                    PercentDescount = percent,
                    TipoDesconto = client,
                    IsActive = true,
                    CreateAt = DateTime.UtcNow

                };
            }
        }

    }
}
