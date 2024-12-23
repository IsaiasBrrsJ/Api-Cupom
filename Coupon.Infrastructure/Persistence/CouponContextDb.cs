using Coupon.Core.Entities.Client;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Event;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Coupon.Infrastructure.Persistence
{
    public class CouponContextDb : DbContext
    {
        public CouponContextDb(DbContextOptions<CouponContextDb> options) : base(options)
        { }
        
        public DbSet<Core.Entities.Coupon.Coupon> Coupon { get; set; }
        public DbSet<Client> Clients { get; set; }  
        public DbSet<Discount> Descounts { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Events<EventBase>> EventsDomain { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var model in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetProperties()
            .Where(p => p.ClrType == typeof(string))))
                model.SetMaxLength(100);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CouponContextDb).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
