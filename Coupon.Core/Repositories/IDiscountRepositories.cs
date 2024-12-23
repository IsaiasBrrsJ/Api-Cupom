using Coupon.Core.Entities.Coupon;

namespace Coupon.Core.Repositories;

public interface IDiscountRepositories
{
    Task<Guid> Create(Discount descount);
    Task<Discount> FindByIdEntity(Guid id);
    Task<Discount> GetByIdDapper(Guid id);
    void Update(Discount descount);
}
