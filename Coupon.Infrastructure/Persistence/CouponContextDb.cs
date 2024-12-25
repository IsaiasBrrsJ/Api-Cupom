using Coupon.Core.Entities.Client;
using Coupon.Core.Entities.Coupon;
using Coupon.Core.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Text;

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

        public override int SaveChanges()
        {

           // var entries = ChangeTracker.Entries()
           //.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
           //.ToList();

           // foreach (var entry in entries)
           // {
           //     var audit = new Audit
           //     {
           //         EntityName = entry.Entity.GetType().Name,
           //         EntityId = (int)entry.Property("Id").CurrentValue, // Supondo que a chave primária é "Id"
           //         Action = entry.State.ToString(),
           //         Date = DateTime.UtcNow,
           //         Details = GetEntityDetails(entry)
           //     };

           //     Audits.Add(audit);
           // }

            return base.SaveChanges();
        }
        private string GetEntityDetails(EntityEntry entry)
        {
            var details = new StringBuilder();
            foreach (var property in entry.OriginalValues.Properties)
            {
                var originalValue = entry.OriginalValues[property];
                var currentValue = entry.CurrentValues[property];
                details.AppendLine($"{property.Name}: {originalValue} => {currentValue}");
            }
            return details.ToString();
        }
    }


}
