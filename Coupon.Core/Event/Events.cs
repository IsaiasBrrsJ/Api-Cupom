namespace Coupon.Core.Event
{
    public class Events<T> : EventBase where T : class
    {
        private Events()
        {
        }
        public static class Factories
        {
            public static  Events<T> Create(Guid idRequest, string @operator, string reason, string eventType)
            {
                return new Events<T>
                {
                    Id =Guid.NewGuid(),
                    DomainId = idRequest,
                    @operator = @operator,
                    Reason = reason,
                    LastUpdate = DateTime.UtcNow,
                    EventType = eventType
                };
            }
        }
    }
}
