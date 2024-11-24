using Coupon.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coupon.Infrastructure
{
    public static class InfrastructureModules
    {

        public static IServiceCollection AddInfra(this IServiceCollection service, IConfiguration configuration)
        {
            service
                .AddDb(configuration);

            return service;
        }


        private static IServiceCollection AddDb(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<CouponContextDb>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("BdEstudos"));
                x.EnableDetailedErrors();
            });

            return service;
        }

    }
}
