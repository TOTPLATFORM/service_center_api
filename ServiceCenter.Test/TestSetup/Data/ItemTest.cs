using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;


public static class ItemTest
{

    public static void AddItem(this ServiceCenterBaseDbContext context)
    {
        context.Items.AddRange(
        new Item
        {
            Id = 1,
            ItemName = "Item 1",
            ItemDescription = "Desc Of Item 1",
            ItemStock = 1000,
            ItemPrice = 50,
          
        },
        new Item
        {
            Id = 2,
            ItemName = "Item 2",
            ItemDescription = "Desc Of Item 2",
            ItemStock = 500,
            ItemPrice = 20,
          
        },
        new Item
        {
            Id = 3,
            ItemName = "Item 3",
            ItemDescription = "Desc Of Item 3",
            ItemStock = 200,
            ItemPrice = 30,
          
        }
        );
    }
}
