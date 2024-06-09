using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class RoleUserConfigurations : IEntityTypeConfiguration<IdentityRole>
{
	public void Configure(EntityTypeBuilder<IdentityRole> builder)
	{
		builder.HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() },
		   new IdentityRole { Name = "Customer", NormalizedName = "Customer".ToUpper() },
		   new IdentityRole { Name = "Employee", NormalizedName = "Employee".ToUpper() },
		   new IdentityRole { Name = "Sales", NormalizedName = "Sales".ToUpper() },
		   new IdentityRole { Name = "WarehouseManager", NormalizedName = "WarehouseManager".ToUpper() },
		   new IdentityRole { Name = "Manager", NormalizedName = "Manager".ToUpper() },
           new IdentityRole { Name = "ServiceProvider", NormalizedName = "ServiceProvider".ToUpper() },
           new IdentityRole { Name = "Vendor", NormalizedName = "Vendor".ToUpper() },
           new IdentityRole { Name = "Contact", NormalizedName = "Contact".ToUpper() }

           );
	}
}