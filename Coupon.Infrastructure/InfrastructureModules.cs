using Coupon.Core.Repositories;
using Coupon.Infrastructure.Persistence;
using Coupon.Infrastructure.Persistence.Repositories;
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
                .AddDb(configuration)
                .AddRepositories()
                .AddPatterns();

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

        private static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            

            service
                .AddTransient<IClientRepositories, ClientRepositories>()
                .AddTransient<ICouponRepositories, CouponRepositories>();
                
            return service;
        }

        private static IServiceCollection AddPatterns(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            return service;
        }

    }
}
