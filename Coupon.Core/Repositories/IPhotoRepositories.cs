using Coupon.Core.Entities.Coupon;

namespace Coupon.Core.Repositories
{
    public interface IPhotoRepositories
    {
        Task<Guid> AddAsync(Photo photo);
        Task<IEnumerable<Photo>> GetAllAsync();
        Task<Photo> GetByIdAsync(Guid id);
        Task UpdateAsync(Photo photo);
    }
}
