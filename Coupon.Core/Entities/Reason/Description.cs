using Coupon.Core.Entities.Coupon;

namespace Coupon.Core.Entities.Reason
{
    public class Description : EntityBase
    {
        public DateTime LastUpdate { get; private set; }
        public string @operator { get; private set; } = string.Empty;
        public string Reason { get; private set; } = string.Empty;

        public virtual ICollection<Client.Client>? Clients { get;  init; }
        public virtual ICollection<Descount>? Descounts{ get; init; }
        public virtual ICollection<Coupon.Coupon>? Coupons { get; init; }

        public static class Factories
        {
            public static Description Create(string reason, string @operator)
            {
                return new Description
                {
                    Reason = reason,
                    @operator = @operator,
                    LastUpdate = DateTime.UtcNow
                };
            }
        }
    }
}
