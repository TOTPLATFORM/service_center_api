using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Infrastructure.Configuration;

public class WareHouseManagerConfigurations : IEntityTypeConfiguration<WareHouseManager>
{
    public void Configure(EntityTypeBuilder<WareHouseManager> builder)
    {
        builder.ToTable("WareHouseManagers");

    }
}