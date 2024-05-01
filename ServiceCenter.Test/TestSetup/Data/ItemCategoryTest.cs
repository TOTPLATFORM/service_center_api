using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;
[TestCaseOrderer(
ordererTypeName: "ServiceCenter.Test.TestPriority.PriorityOrderer",
ordererAssemblyName: "ServiceCenter.Test")]
public static class ItemCategoryTest

{
    public static void AddItemCategory(this ServiceCenterBaseDbContext context)
    {
        context.ItemCategories.AddRange(
        new ItemCategory { Id = 1, CategoryName = "First Aid Supplies" },
        new ItemCategory { Id = 2, CategoryName = "Medication" },
        new ItemCategory { Id = 3, CategoryName = "Medical Equipment" }
        );
    }
}
