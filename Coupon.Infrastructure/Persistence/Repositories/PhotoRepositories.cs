using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;

namespace Coupon.Infrastructure.Persistence.Repositories
{
    public class PhotoRepositories : IPhotoRepositories
    {
        private readonly CouponContextDb _DbContext;
        public PhotoRepositories(CouponContextDb dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<Guid> AddAsync(Photo photo)
        {
          await _DbContext.Photos.AddAsync(photo);

          return photo.Id;
        }

        public Task<IEnumerable<Photo>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Photo> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}
