using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class SalesTest
{
    public static void AddSales(this ServiceCenterBaseDbContext context)
    {
        context.Sales.AddRange(
        new Sales
        {
            Id = "0d133c1a-804f-4508-8f7e-8c3f504844e0",
            Email = "agershaban7@gmail.com",
            PhoneNumber = "0621654984",


        },
        new Sales
        {
            Id = "sOO316984165",
            Email = "agershaban7@gmail.com",
            PhoneNumber = "0621654984"

        }
        );
    }
    
}
