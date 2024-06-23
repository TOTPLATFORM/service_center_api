using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ServicePackageTest
{
    public static void AddServicePackage(this ServiceCenterBaseDbContext context)
    {
        context.ServicePackages.AddRange(
        new ServicePackage
        {
            Id = 1,
            PackageName="Package1",
            PackageDescription="PackageDesc1",
            PackagePrice=300
            
        },
         new ServicePackage
         {
             Id = 2,
             PackageName = "Package2",
             PackageDescription = "PackageDesc2",
             PackagePrice = 300

         }
        );
    }
}
