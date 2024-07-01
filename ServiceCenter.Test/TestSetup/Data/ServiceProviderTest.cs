using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ServiceProviderTest
{
    public static void AddServiceProvider(this ServiceCenterBaseDbContext context)
    {
        context.ServiceProviders.AddRange(
        new ServiceProvider
        {
            Id = "0d133c1a-804f-4548-8f7e-8c3f004844e0",
            Email = "agershaban7@gmail.com",
            PhoneNumber = "0621654984",


        },
        new ServiceProvider
        {
            Id = "sOB316984195",
            Email = "agershaban7@gmail.com",
            PhoneNumber = "0621654984"

        }
        );
    }
}
