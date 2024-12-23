using Coupon.Application.Command.Coupon;
using Coupon.Application.Command.Discount;
using Coupon.Application.Handler.Coupon;
using Coupon.Application.Handler.Discount;
using Coupon.Application.Query.Coupon;
using Coupon.Application.Query.Discount;
using Coupon.Application.Services.Command.Clients;
using Coupon.Application.Services.Coupons;
using Coupon.Core.Abstractions;
using Coupon.Core.BaseResult;
using Coupon.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Coupon.Application
{
    public static class ApplicationModules
    {
        public static IServiceCollection AddApplicationsModules(this IServiceCollection services)
        {

            services
                .AddPatternCQRS()
               .AddServicesApplication();

            return services;
        }

        private static IServiceCollection AddPatternCQRS(this IServiceCollection services)
        {
            //Command Create/Insert

            services.AddTransient<ICommandHandler<CreateCouponCommand>, CreateCouponCommandHandler>();
            services.AddTransient<ICommandHandler<CreateDiscountCommand>, CreateDiscountCommandHandler>();
            services.AddTransient<ICommandHandler<InsertPhotoCommand>, InsertPhotoCommandHandler>();

            //Command Update
            
            services.AddTransient<ICommandHandler<UpdatePriceCouponCommand>, UpdatePriceCouponCommandHandler>();
            services.AddTransient<ICommandHandler<SetCouponExpiredCommand>, SetCouponExpiredCommandHandler>();
            services.AddTransient<ICommandHandler<UpdatePhotoCommand>, UpdatePhotoCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateDateValidateCommand>, UpdateDateValidateCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateDiscountPercentCommand>, UpdateDiscountPercentCommandHandler>();


            //Queries
            
            services.AddTransient<IQueryHanlder<GetCouponById, ResultViewModel>, GetCoupounByIdQueryHandler>();
            services.AddTransient<IQueryHanlder<GetAllCoupon, ResultViewModel>, GetAllCouponQueryHandler>();
            services.AddTransient<IQueryHanlder<GetDiscountById, ResultViewModel>, GetDiscountByIdQueryHandler>();
           
            return services;
        }
        private static IServiceCollection AddServicesApplication(this IServiceCollection services)
        {

            services
                .AddScoped<IClientService, ClienteService>()
                .AddScoped<ICouponService, CouponService>();
                

            return services;
        }
    }
}
