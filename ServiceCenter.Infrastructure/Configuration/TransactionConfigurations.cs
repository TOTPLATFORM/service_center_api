using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class TransactionConfigurations:IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(p => p.TransactionType)
               .IsRequired();
        builder.Property(p => p.Quantity)
              .IsRequired();
        builder.Property(p => p.TransactionDate)
              .IsRequired();

    }
}
