namespace Coupon.Core.Entities
{
    public abstract class EntityBase
    {
        public  Guid Id { get;  } = Guid.NewGuid();
    }
}
