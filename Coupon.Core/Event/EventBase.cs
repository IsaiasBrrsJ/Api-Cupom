using Coupon.Core.Entities;

namespace Coupon.Core.Event
{
    public abstract class EventBase : EntityBase
    {
        public Guid DomainId { get; init; } = default!;
        public DateTime LastUpdate { get; init; }
        public string @operator { get; init; } = default!;
        public string Reason { get; init; } = default!;
        public string EventType { get; init; } = default!;

       
    }
}
