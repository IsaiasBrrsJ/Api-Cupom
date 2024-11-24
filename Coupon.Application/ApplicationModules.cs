using Coupon.Application.Services;
using Coupon.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Coupon.Application
{
    public static class ApplicationModules
    {
        public static IServiceCollection AddApplicationsModules(this IServiceCollection services)
        {

            services
               .AddServicesApplication();

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
