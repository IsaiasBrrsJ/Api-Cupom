using Coupon.Application.Command.Coupon;
using Coupon.Application.Extension;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;
using Coupon.Core.Services;

namespace Coupon.Application.Handler.Coupon;

public class UpdatePhotoCommandHandler : ICommandHandler<UpdatePhotoCommand>
{
    private readonly ICouponService _couponService;
    private readonly ICouponRepositories _couponRepositories;
    private readonly IEventRepositories<Core.Entities.Coupon.Coupon> _eventStore;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePhotoCommandHandler(
        ICouponService couponService,
        ICouponRepositories couponRepositories, 
        IEventRepositories<Core.Entities.Coupon.Coupon> eventStore, 
        IUnitOfWork unitOfWork)
    {
        _couponService = couponService;
        _couponRepositories = couponRepositories;
        _eventStore = eventStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel> ExecuteAsync(UpdatePhotoCommand command)
    {
        if ((command == null || command.photo == null ) || !command.idCoupon.IsGuid())
            return ResultViewModel.Failure("Fail on update");


        var coupon = await _couponRepositories.FindByIdEntityAsync(command.idCoupon);

        if (coupon.Photo == null)
            return ResultViewModel.Failure("Photo notFound");

        var deletePhotoFromBlob =await _couponService.DeleteImage(coupon.Id);
        
        if(!deletePhotoFromBlob)
            return ResultViewModel.Failure("Error on  delete photo");


        var infoBlob = await _couponService.SendImageToBlobStorage(command.photo!, coupon.Id);

        var photo = Photo.Factories.Update(coupon.Photo.Id, infoBlob.fileName, infoBlob.blobUrl, command.photo.ContentType, coupon.Id);

        coupon.UpdatePhoto(command.reason, command.@operator);
        coupon.Photo = photo;

        await _eventStore.AddAsync(coupon.eventsRead);
        _couponRepositories.UpdateAsync(coupon);

       
        await _unitOfWork.Commit();


        return ResultViewModel.Success("Picture updated with success");
    }
}
