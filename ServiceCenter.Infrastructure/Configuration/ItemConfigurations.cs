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
                .IsRequired();
        builder.Property(T => T.ItemDescription)
               .IsRequired();
        builder.Property(T => T.ItemPrice)
            .IsRequired();
        builder.Property(T => T.ItemStock)
            .IsRequired();
        builder.Property(T => T.CategoryId)
            .IsRequired();
    }
}
