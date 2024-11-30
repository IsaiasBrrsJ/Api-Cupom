using Coupon.Core.Entities.Coupon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coupon.Infrastructure.Persistence.Configurations
{
    public class DescountConfigurations : IEntityTypeConfiguration<Descount>
    {

        public void Configure(EntityTypeBuilder<Descount> builder)
        {
          builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

          
        }
    }
}
