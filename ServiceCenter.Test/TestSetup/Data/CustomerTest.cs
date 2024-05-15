using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class CustomerTest
{
    public static void AddCustomer(this ServiceCenterBaseDbContext context)
    {
        context.Customers.AddRange(
        new Customer
        {
            Id = "0d133c1a-804f-4548-8f7e-8c3f504561954",
            BranchId = 1,   
        }
        );
    }
}
