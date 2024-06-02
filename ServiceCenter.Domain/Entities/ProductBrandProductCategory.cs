using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class ProductBrandProductCategory
{
    public int ProductBrandId { get; set; }
    public virtual ProductBrand ProductBrand { get; set; }

    public int ProductCategoryId { get; set; }
    public virtual ProductCategory ProductCategory { get; set; }
}
