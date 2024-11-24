using Coupon.Core.Entities.Coupon;

namespace Coupon.Core.Entities.Reason
{
    public class Description : EntityBase
    {
        public DateTime LastUpdate { get; private set; }
        public string @operator { get; private set; } = string.Empty;
        public string Reason { get; private set; } = string.Empty;
        public virtual Coupon.Coupon Coupon { get; init; } = default!;
        public Guid? CouponId { get; init; }
        public virtual Client.Client Client { get; init; } = default!;
        public Guid? ClientId { get; init; }
        public virtual Descount Descount { get; init; } = default!;
        public Guid? DescountId { get; init; }
       
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
