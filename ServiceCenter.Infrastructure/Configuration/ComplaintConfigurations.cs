using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ComplaintConfigurations : IEntityTypeConfiguration<Complaint>
{
    public void Configure(EntityTypeBuilder<Complaint> builder)
    {
        builder.Property(T => T.CreatedDate)
               .IsRequired();
      
        builder.Property(T => T.ComplaintStatus)
              .IsRequired()
;
		builder.Property(T => T.ComplaintDescription)
              .IsRequired()
			  .HasColumnType("varchar")
			  .HasMaxLength(100);

        builder.Property(T=>T.ComplaintCategory)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar");

    }
}