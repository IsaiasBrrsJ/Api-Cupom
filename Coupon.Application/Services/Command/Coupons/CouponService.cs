using Coupon.Application.ViewModel.Coupon;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Http;

namespace Coupon.Application.Services.Command.Coupons
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

        public async Task DeactivateCoupon(Guid coupon, string reason, string @operator)
        {
           var client = await GetClientById(coupon);
          
            client.Deactivate(reason, @operator);

           await _eventStore.AddAsync(client.eventsRead);

           await _unitOfWork.Commit();
        }

        public Task<IEnumerable<Core.Entities.Coupon.Coupon>> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public async Task<Core.Entities.Coupon.Coupon> GetClientById(Guid id)
        {
           return await _couponRepositories.GetByIdAsync(id);
        }

        public  async Task<ResultViewModel> InsertCoupon(Core.Entities.Coupon.Coupon coupon, IFormFile? photo)
        {
            Guid userId = default!;

           if(photo is not null)
            {
                var infoBlob = await _blobStorageService.UploadPhoto(photo!, coupon.Id);
                var photoSend = Photo.Factories.Create(infoBlob.fileName, infoBlob.blobUrl, photo!.ContentType, coupon.Id);

                userId = await _couponRepositories.AddAsync(coupon);
                await _photoRepositories.AddAsync(photoSend);

                await _unitOfWork.Commit();

                var resultModels = CouponViewModel.Factories.CreateWithPhoto(infoBlob.blobUrl, coupon.EventDate, Enum.GetName(typeof(CouponType), coupon.TypeCoupon)!, userId);

                return ResultViewModel<CouponViewModel>.Success(resultModels, "Coupon and Photo Inserted With Success");
            }

             userId = await _couponRepositories.AddAsync(coupon);

            await _unitOfWork.Commit();

            var resultModel = CouponViewModel.Factories.Create(coupon.EventDate, Enum.GetName(typeof(CouponType), coupon.TypeCoupon)!, userId);

            return ResultViewModel<CouponViewModel>.Success(resultModel, "Cupom com sucesso");
        }

      

        public async Task<(string blobUrl, string fileName)> SendImageToBlobStorage(IFormFile photo, Guid idCoupon)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCoupon(Core.Entities.Coupon.Coupon coupon)
        {

        }

    }
}
