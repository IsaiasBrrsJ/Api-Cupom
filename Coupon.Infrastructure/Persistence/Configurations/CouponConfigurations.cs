using Coupon.Core.Entities.Coupon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coupon.Infrastructure.Persistence.Configurations
{
    internal class CouponConfigurations : IEntityTypeConfiguration<Core.Entities.Coupon.Coupon>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Coupon.Coupon> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Price)
                  .HasColumnType("decimal(10,2)");

            builder
                .HasOne(x => x.Photo)
                .WithOne(x => x.Coupon)
                .HasForeignKey<Photo>(x => x.CouponId);
                
        }
    }
}
