using Coupon.Application.Command.Coupon;
using Coupon.Application.Handler.Coupon;
using Coupon.Application.Services.Command.Clients;
using Coupon.Application.Services.Coupons;
using Coupon.Core.Abstractions;
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
            services.AddTransient<ICommandHandler<CreateCouponCommand>, CreateCouponCommandHandler>();
            services.AddTransient<ICommandHandler<InsertPhotoCommand>, InsertPhotoCommandHandler>();
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
