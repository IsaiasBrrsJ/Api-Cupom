﻿using Coupon.Core.Entities.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coupon.Infrastructure.Persistence.Configurations
{
    public class ClientConfigurations : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder
                .HasOne(x => x.Description)
                .WithMany(x => x.Clients)
                .HasForeignKey(x => x.DescriptionId);
        }
    }
}