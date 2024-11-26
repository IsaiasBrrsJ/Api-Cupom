using Coupon.Core.Entities.Client;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Http;

namespace Coupon.Application.Services.Command.Coupons
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepositories _couponRepositories;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;

        public CouponService(ICouponRepositories couponRepositories, IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _couponRepositories = couponRepositories;
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public Task DeactivateCoupon(Guid coupon)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Entities.Coupon.Coupon>> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Entities.Coupon.Coupon>> GetClientById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> InsertCoupon(Core.Entities.Coupon.Coupon coupon, IFormFile photo)
        {
            var (blobUrl, fileName) = (string.Empty, string.Empty);

            Core.Entities.Coupon.Coupon cuponEntity = null!;

            if (photo != null)
            {
                (blobUrl, fileName) = await SendImageToBlobStorage(photo, coupon.Id);

                cuponEntity = Core.Entities.Coupon.Coupon.Factories.CreateWithPhoto(coupon.TypeCoupon, coupon.Price, coupon.ValidAt, coupon.EventDate, coupon.MaxCoupon,
                    Photo.Factories.Create(fileName, blobUrl, photo!.ContentType, coupon.Id)
                    );
            
                var userId = await _couponRepositories.AddAsync(cuponEntity);

                return userId;
            }


            var id = await _couponRepositories.AddAsync(coupon);

            return id;

        }

        internal async Task<(string blobUrl, string fileName)> SendImageToBlobStorage(IFormFile photo, Guid idCoupon)
        {
            var blobInfo = await _blobStorageService.UploadPhoto(photo, idCoupon);

            return (blobInfo.blobUrl, blobInfo.fileName);
        }

        public async Task UpdateCoupon(Core.Entities.Coupon.Coupon coupon)
        {

        }

    }
}
