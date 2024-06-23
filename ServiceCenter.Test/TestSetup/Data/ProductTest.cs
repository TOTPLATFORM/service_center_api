using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ProductTest
{
    public static void AddProduct(this ServiceCenterBaseDbContext context)
    {
        context.Products.AddRange(
        new Product
        {
            Id = 1,
            ProductName = "product1",
            ProductDescription = "product is good",
            ProductPrice = 20,
        },
        new Product
        {
            Id = 2,
            ProductName = "product1",
            ProductDescription = "product is good",
            ProductPrice = 20,
        }
        );
    }
}
