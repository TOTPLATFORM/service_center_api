using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class ItemConfigurations : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.Property(T => T.ItemName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);

        builder.Property(T => T.ItemDescription)
               .IsRequired()
               .HasColumnType("varchar")
               .HasMaxLength(100);

        builder.Property(T => T.ItemPrice)
            .IsRequired();

        builder.Property(T => T.ItemStock)
            .IsRequired();

    }
}
