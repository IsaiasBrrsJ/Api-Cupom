using Coupon.Application.Command.Coupon;
using Coupon.Application.Extension;
using Coupon.Application.Services.Command.Clients;
using Coupon.Application.ViewModel.Coupon;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Repositories;
using Coupon.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.Application.Handler.Coupon;

public class CreateCouponCommandHandler : ICommandHandler<CreateCouponCommand>
{
    private readonly ICouponRepositories _couponRepositories;
    private readonly IEventRepositories<Core.Entities.Coupon.Coupon> _eventStore;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IPhotoRepositories _photoRepositories;
    private readonly ICouponService _couponService;

    public CreateCouponCommandHandler(
        ICouponRepositories couponRepositories,
        IEventRepositories<Core.Entities.Coupon.Coupon> eventStore,
        IUnitOfWork unitOfWork, IBlobStorageService blobStorageService,
        IPhotoRepositories photoRepositories,
        ICouponService couponService)
    {
        _couponRepositories = couponRepositories;
        _eventStore = eventStore;
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
        _photoRepositories = photoRepositories;
        _couponService = couponService;
    }

    public async Task<ResultViewModel> ExecuteAsync(CreateCouponCommand command)
    {
        if (command.File is null)
            return await Insert(command);


        return await InsertWithPhoto(command);
    }

    private async Task<ResultViewModel> Insert(CreateCouponCommand command)
    {
        var coupon = command.ToEntity();

        var result = await _couponRepositories.AddAsync(coupon);
       
        await _unitOfWork.Commit();


        if (!result.IsGuid())
            return ResultViewModel.Failure("Falha ao criar user");

        var couponTypeName = Enum.GetName(typeof(CouponType), coupon.TypeCoupon)!;

        var viewModel = CouponViewModel.Factories.Create(DateTime.Now, couponTypeName, result);

        return ResultViewModel<CouponViewModel>.Success(viewModel, "User Created With Success");
    }
    private async Task<ResultViewModel> InsertWithPhoto(CreateCouponCommand command)
    {

        var coupon = command.ToEntity();

        var infoBlob = await _couponService.SendImageToBlobStorage(command.File!, coupon.Id);
        var photo = Photo.Factories.Create(infoBlob.fileName, infoBlob.blobUrl,command.File!.ContentType, coupon.Id);
        
        var idPhoto =await _photoRepositories.AddAsync(photo);

        coupon.InsertEvent("System", "Coupon with photo created");

        await _eventStore.AddAsync(coupon.eventsRead);

        var idCoupon =  await _couponRepositories.AddAsync(coupon);
       
        await _unitOfWork.Commit();


        if (!idCoupon.IsGuid() || !idPhoto.IsGuid())
            return ResultViewModel.Failure("Fail");

        var resultModels = CouponViewModel.Factories.CreateWithPhoto(infoBlob.blobUrl, coupon.EventDate, Enum.GetName(typeof(CouponType), coupon.TypeCoupon)!, coupon.Id);

        return ResultViewModel<CouponViewModel>.Success(resultModels, "Coupon and Photo Inserted With Success");

    }
}
