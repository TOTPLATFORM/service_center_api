 using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static  class ServiceCategoryTest
{
    public static void AddServiceCategory(this ServiceCenterBaseDbContext context)
    {
        context.ServiceCategories.AddRange(
        new ServiceCategory
        {
            Id = 1,
            ServiceCategoryName = "Category1",
           ServiceCategoryDescription = "Description1"
        },
        new ServiceCategory
        {
            Id = 2,
            ServiceCategoryName = "Category2",
            ServiceCategoryDescription = "Description2",
        }
        );
    }
}
