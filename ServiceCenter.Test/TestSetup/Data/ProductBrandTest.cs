using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ProductBrandTest
{
    public static void AddProductBrand(this ServiceCenterBaseDbContext context)
    {
        context.ProductBrands.AddRange(
        new ProductBrand
        {
            Id = 1,
            BrandName = "Brand1",
            BrandDescription = "Brand is good",
            FoundedYear = DateOnly.Parse("2000/12/30"),
            CountryOfOrigin =Country.Canada
        },
        new ProductBrand
        {
            Id = 2,
            BrandName = "Brand2",
            BrandDescription = "Brand is good",
            FoundedYear = DateOnly.Parse("2000/12/30"),
            CountryOfOrigin = Country.France
        }
        );
    }
}
