using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceCenter.Infrastructure.Configuration;

public class OfferConfigurations : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.Property(p => p.OfferName)
               .IsRequired()
               .HasColumnType("varchar")
              .HasMaxLength(50);

        builder.Property(p => p.OfferDescription)
              .HasMaxLength(150)
              .HasColumnType("varchar")
               .IsRequired();

        builder.Property(p => p.StartDate)
               .IsRequired();

        builder.Property(p => p.EndDate)
               .IsRequired();

        builder.Property(p => p.Discount)
               .IsRequired();
        

    }
}