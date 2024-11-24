using Coupon.Core.Entities.Reason;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coupon.Infrastructure.Persistence.Configurations
{
    public class DescriptionConfigurations : IEntityTypeConfiguration<Description>
    {
        public void Configure(EntityTypeBuilder<Description> builder)
        {
            builder.HasKey(x => x.Id); 


            builder.Property(x => x.Id)
                .ValueGeneratedNever();


            builder.
                HasOne(x => x.Client)
                .WithMany(x => x.Descriptions)
                .HasForeignKey(x => x.ClientId);


            builder.
                HasOne(x => x.Coupon)
                .WithMany(x => x.Descriptions)
                .HasForeignKey(x => x.CouponId);


            builder.
                HasOne(x => x.Descount)
                .WithMany(x => x.Descriptions)
                .HasForeignKey(x => x.DescountId);
        }
    }
}
