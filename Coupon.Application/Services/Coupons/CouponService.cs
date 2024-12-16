using Coupon.Application.ViewModel.Coupon;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Event;
using Coupon.Core.Repositories;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Http;
using static Azure.Core.HttpHeader;

namespace Coupon.Application.Services.Coupons
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepositories _couponRepositories;
        private readonly IEventRepositories<Core.Entities.Coupon.Coupon> _eventStore;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IPhotoRepositories _photoRepositories;

        public CouponService(ICouponRepositories couponRepositories, IEventRepositories<Core.Entities.Coupon.Coupon> eventStore, IUnitOfWork unitOfWork, IBlobStorageService blobStorageService, IPhotoRepositories photoRepositories)
        {
            _couponRepositories = couponRepositories;
            _eventStore = eventStore;
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
            _photoRepositories = photoRepositories;
        }
        public Task<IEnumerable<Core.Entities.Coupon.Coupon>> GetAllClients()
        {
            throw new NotImplementedException();
        }
        public async Task<Core.Entities.Coupon.Coupon> GetClientById(Guid id)
        {
            return await _couponRepositories.GetByIdAsync(id);
        }

        internal async Task<Guid> InsertPhoto(IFormFile photo, string blobUrl, string blobFileName, Guid couponId)
        {
            var photoSend = Photo.Factories.Create(blobFileName, blobUrl, photo!.ContentType, couponId);

            return await _photoRepositories.AddAsync(photoSend);
        }

        public async Task<(string blobUrl, string fileName)> SendImageToBlobStorage(IFormFile photo, Guid idCoupon)
        {
            var infoBlob = await _blobStorageService.UploadPhoto(photo!, idCoupon);

            return infoBlob;
        }
        public async Task UpdateCoupon(Core.Entities.Coupon.Coupon coupon)
        {

        }

    }
}
