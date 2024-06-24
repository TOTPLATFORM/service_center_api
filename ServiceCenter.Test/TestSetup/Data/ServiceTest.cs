using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ServiceTest
{

    public static void AddService(this ServiceCenterBaseDbContext context)
    {
        context.Services.AddRange(
        new Service
        {
            Id = 1,
            ServiceName="Service1",
            ServiceDescription="ServiceDesc1",
            Avaliable=Status.Pending,
            CenterId=1,
            ServiceCategoryId=1 
        },
        new Service
        {
            Id = 2,
            ServiceName = "Service2",
            ServiceDescription = "ServiceDesc2",
            Avaliable = Status.Pending,
            CenterId = 1,
            ServiceCategoryId = 1
        }
        );
    }
}
