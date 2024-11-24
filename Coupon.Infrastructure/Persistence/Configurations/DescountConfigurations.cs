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

            builder
             .HasOne(x => x.Description)
             .WithMany(x => x.Descounts)
             .HasForeignKey(x => x.DescriptionId);
        }
    }
}
