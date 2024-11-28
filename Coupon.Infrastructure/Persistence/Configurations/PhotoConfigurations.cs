using Coupon.Core.Entities.Coupon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coupon.Infrastructure.Persistence.Configurations
{
    public class PhotoConfigurations : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();


            builder
                 .HasOne(x => x.Coupon)
                 .WithOne(x => x.Photo)
                 .HasForeignKey<Photo>(x => x.CouponId);
          
        }
    }
}
