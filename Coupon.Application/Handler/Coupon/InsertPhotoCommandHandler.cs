using Coupon.Application.Command.Coupon;
using Coupon.Application.Extension;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;
using Coupon.Core.Services;

namespace Coupon.Application.Handler.Coupon;

public class InsertPhotoCommandHandler : ICommandHandler<InsertPhotoCommand>
{
    private readonly ICouponService _couponService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IEventRepositories<Core.Entities.Coupon.Photo> _eventStore;
    private readonly IPhotoRepositories _photoRepositories;
    private readonly ICouponRepositories _couponRepositories;

    public InsertPhotoCommandHandler(ICouponService couponService,
        IUnitOfWork unitOfWork, 
        IBlobStorageService blobStorageService, 
        IPhotoRepositories photoRepositories,
        ICouponRepositories couponRepositories,
        IEventRepositories<Core.Entities.Coupon.Photo> eventStore)
    {
        _couponService = couponService;
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
        _photoRepositories = photoRepositories;
        _couponRepositories = couponRepositories;
        _eventStore = eventStore;
    }

    public async Task<ResultViewModel> ExecuteAsync(InsertPhotoCommand command)
    {
        if (command.photo is null || !command.couponId.IsGuid())
            return  ResultViewModel.Failure("");

        var coupon = await _couponRepositories.GetByIdAsync(command.couponId);

        if (!coupon.Id.IsGuid())
            return ResultViewModel.Failure("User não encontrado");


       var photoInfo = await _couponService.SendImageToBlobStorage(command.photo, command.couponId);

       var photo = Photo.Factories.Create(photoInfo.fileName, photoInfo.blobUrl, command.photo.ContentType, coupon.Id);

       await _photoRepositories.AddAsync(photo);

        photo.InsertEvent("System", "Photo Inserted");

        await _eventStore.AddAsync(photo.eventsRead);

       await _unitOfWork.Commit();

     
       return ResultViewModel.Success("Success");
    }
}
