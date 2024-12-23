using Coupon.Core.Entities.Coupon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coupon.Infrastructure.Persistence.Configurations
{
    public class DiscountConfigurations : IEntityTypeConfiguration<Discount>
    {

        public void Configure(EntityTypeBuilder<Discount> builder)
        {
          builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();


            builder.Property(X => X.TipoDesconto)
                .HasConversion<string>();


            builder.Property(x => x.PercentDescount)
            .HasColumnType("decimal(10,2)");
        }
    }
}
