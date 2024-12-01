using Coupon.Core.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System.Reflection.Emit;

namespace Coupon.Infrastructure.Persistence.Configurations
{
    public class EventBaseConfigurations: IEntityTypeConfiguration<EventBase>
    {
        public void Configure(EntityTypeBuilder<EventBase> builder)
        {
            builder
                 .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();


            builder
                .Property(x => x.DomainId)
                .IsRequired()
                .HasColumnType("varchar(256)");

            builder
            .HasIndex(e => e.DomainId)
            .HasDatabaseName("IX_DomainId");

            builder
           .HasIndex(e => e.EventType)
           .HasDatabaseName("IX_EventType");

            builder
                .HasDiscriminator(x => x.EventType);
        }

    }
}
