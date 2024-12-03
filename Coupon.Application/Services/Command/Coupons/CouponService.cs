using Coupon.Application.ViewModel.Coupon;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Event;
using Coupon.Core.Repositories;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Http;
using static Azure.Core.HttpHeader;

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
            try
            {
                Guid userId = default!;

                if (photo is not null)
                {
                    var infoBlob = await SendImageToBlobStorage(photo, coupon.Id);

                    var idPhoto = InsertPhoto(photo, infoBlob.blobUrl, infoBlob.fileName, coupon.Id);
                   
                    coupon.InsertEvent("System", "Coupon with photo created");

                    await _eventStore.AddAsync(coupon.eventsRead);

                    await _couponRepositories.AddAsync(coupon);

                    await _unitOfWork.Commit();

                    var resultModels = CouponViewModel.Factories.CreateWithPhoto(infoBlob.blobUrl, coupon.EventDate, Enum.GetName(typeof(CouponType), coupon.TypeCoupon)!, coupon.Id);

                    return ResultViewModel<CouponViewModel>.Success(resultModels, "Coupon and Photo Inserted With Success");
                }

                coupon.InsertEvent("System", "Coupon created");

                userId = await _couponRepositories.AddAsync(coupon);

                await _eventStore.AddAsync(coupon.eventsRead);

                await _unitOfWork.Commit();

                var resultModel = CouponViewModel.Factories.Create(coupon.EventDate, Enum.GetName(typeof(CouponType), coupon.TypeCoupon)!, userId);

                return ResultViewModel<CouponViewModel>.Success(resultModel, "Cupom inserido com sucesso");
            }
            catch (Exception ex) {
                
                await _unitOfWork.Rollback();
                
                if(photo is not null)
                 await _blobStorageService.DeletePhoto(coupon.Id);

                return ResultViewModel.Failure(ex.Message);
            }
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
