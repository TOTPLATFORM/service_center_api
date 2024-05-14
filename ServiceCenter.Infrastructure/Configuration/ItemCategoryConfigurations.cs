using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class ItemCategoryConfigurations : IEntityTypeConfiguration<ItemCategory>
{
    public void Configure(EntityTypeBuilder<ItemCategory> builder)
    {
       
            builder.Property(T => T.CategoryName)
                   .IsRequired()
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

           builder.Property(T => T.ReferenceNumber)
                  .IsRequired();
    }
}
