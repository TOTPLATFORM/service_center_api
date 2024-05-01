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
public static class ItemTest
{

    public static void AddItem(this ServiceCenterBaseDbContext context)
    {
        context.Items.AddRange(
        new Item
        {
            Id = 1,
            ItemName = "Medical Mask",
            ItemDescription = "Disposable medical face mask",
            ItemStock = 1000,
            ItemPrice = 50,
            CategoryId = 1
        },
        new Item
        {
            Id = 2,
            ItemName = "Antibiotic Ointment",
            ItemDescription = "Topical antibiotic for minor cuts and wounds",
            ItemStock = 500,
            ItemPrice = 20,
            CategoryId = 1
        },
        new Item
        {
            Id = 3,
            ItemName = "Digital Thermometer",
            ItemDescription = "Digital thermometer for measuring body temperature",
            ItemStock = 200,
            ItemPrice = 30,
            CategoryId = 2
        }
        );
    }
}
