using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Infrastructure.Configuration;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(p => p.From)
               .IsRequired()
               .HasColumnType("varchar")
              .HasMaxLength(50);

        builder.Property(p => p.OrderArrivalDate)
               .IsRequired();

        builder.Property(p => p.OrderDate)
              .IsRequired();
        builder.Property(p => p.OrderStatus)
          .IsRequired();

    }
}
