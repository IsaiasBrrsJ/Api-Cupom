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
using FluentValidation;
using Coupon.Application.Validations.Discount;
using FluentValidation.AspNetCore;
using Coupon.Application.Command.Client;
using Coupon.Application.Handler.Clients;
using Coupon.Application.Handler.Client;
using Coupon.Application.Query.Client;

namespace Coupon.Application
{
    public static class ApplicationModules
    {
        public static IServiceCollection AddApplicationsModules(this IServiceCollection services)
        {

            services
                .AddPatternCQRS()
               .AddServicesApplication()
               .AddLibraryValidation();
               

            return services;
        }

        private static IServiceCollection AddPatternCQRS(this IServiceCollection services)
        {
            //Command Create/Insert

            services.AddTransient<ICommandHandler<CreateCouponCommand>, CreateCouponCommandHandler>();
            services.AddTransient<ICommandHandler<CreateDiscountCommand>, CreateDiscountCommandHandler>();
            services.AddTransient<ICommandHandler<InsertPhotoCommand>, InsertPhotoCommandHandler>();
            services.AddTransient<ICommandHandler<CreateClientCommand>, CreateClientCommandHandler>();

            //Command Update
            
            services.AddTransient<ICommandHandler<UpdatePriceCouponCommand>, UpdatePriceCouponCommandHandler>();
            services.AddTransient<ICommandHandler<SetCouponExpiredCommand>, SetCouponExpiredCommandHandler>();
            services.AddTransient<ICommandHandler<UpdatePhotoCommand>, UpdatePhotoCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateDateValidateCommand>, UpdateDateValidateCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateDiscountPercentCommand>, UpdateDiscountPercentCommandHandler>();
            services.AddTransient<ICommandHandler<DisableDiscountCommand>, DisableDiscountCommandHandler>();
            services.AddTransient<ICommandHandler<ActiveDiscountCommand>,ActiveDiscountCommandHandler>();
            services.AddTransient<ICommandHandler<DisableClientCommand>, DisableClientCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateNameCommand>, UpdateNameCommandHandler>();
            services.AddTransient<ICommandHandler<UpdatePhoneNumberCommand>, UpdatePhoneNumberCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateEmailCommand>, UpdateEmailCommandHandler>();

            
            //Queries
            
            services.AddTransient<IQueryHanlder<GetCouponById, ResultViewModel>, GetCoupounByIdQueryHandler>();
            services.AddTransient<IQueryHanlder<GetAllCoupon, ResultViewModel>, GetAllCouponQueryHandler>();
            services.AddTransient<IQueryHanlder<GetDiscountById, ResultViewModel>, GetDiscountByIdQueryHandler>();
            services.AddTransient<IQueryHanlder<GetAllDiscount, ResultViewModel>, GetAllDiscountQueryHandler>();
            services.AddTransient<IQueryHanlder<GetAllClients, ResultViewModel>, GetAllClientsQueryHandler>();
            services.AddTransient<IQueryHanlder<GetByIdClient, ResultViewModel>, GetByIdClientQueryHandler>();
           
            return services;
        }
        private static IServiceCollection AddServicesApplication(this IServiceCollection services)
        {

            services
                .AddScoped<IClientService, ClienteService>()
                .AddScoped<ICouponService, CouponService>();
                

            return services;
        }

        private static IServiceCollection AddLibraryValidation(this IServiceCollection services)
        {

            services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<CreateDiscountCommandValidation>();

            return services;
        }

     
    }
}
