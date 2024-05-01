using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public  class serviceConfigurations : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.Property(T => T.ServiceName)
            .IsRequired();
        builder.Property(T => T.ServiceDescription)
           .IsRequired();
        builder.Property(T => T.ServicePrice)
          .IsRequired();
        builder.Property(T => T.Avaliable)
               .IsRequired();
    }
}
