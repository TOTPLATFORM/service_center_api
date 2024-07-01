using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static  class VendorTest
{
    public static void AddVendor(this ServiceCenterBaseDbContext context)
    {
        context.Vendors.AddRange(
        new Vendor
        {
            Id = "0d133c1a-809f-4548-8f7e-8c3f504844e0",
            Email = "agershaban7@gmail.com",
            PhoneNumber = "0621654984",
            ContractStartDate = DateOnly.Parse("2000/12/30"),
            ContractEndDate = DateOnly.Parse("2000/12/30"),
        },
        new Vendor
        {
            Id = "sOBi16984165",
            Email = "agershaban7@gmail.com",
            PhoneNumber = "0621654984",
            ContractStartDate = DateOnly.Parse("2000/12/30"),
            ContractEndDate = DateOnly.Parse("2000/12/30"),

        }
        );
    }
}
