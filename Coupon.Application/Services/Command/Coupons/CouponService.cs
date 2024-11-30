using Coupon.Application.ViewModel.Coupon;
using Coupon.Core.BaseResult;
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
        private readonly IPhotoRepositories _photoRepositories;

        public CouponService(ICouponRepositories couponRepositories, IUnitOfWork unitOfWork, IBlobStorageService blobStorageService, IPhotoRepositories photoRepositories)
        {
            _couponRepositories = couponRepositories;
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
            _photoRepositories = photoRepositories;
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
