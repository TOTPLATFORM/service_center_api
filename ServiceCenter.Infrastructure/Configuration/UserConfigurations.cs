using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Infrastructure.Configuration;

public class UserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

        builder.UseTptMappingStrategy();

        builder.Property(T => T.FirstName)
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(T => T.LastName)
            .HasColumnType("varchar")
            .HasMaxLength(30);

        builder.Property(T => T.Gender)
            .HasColumnType("varchar")
            .HasMaxLength(20);

        builder.Property(T => T.DateOfBirth)
               .IsRequired();
    }

}
