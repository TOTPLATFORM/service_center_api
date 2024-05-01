using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Product : AuditableEntity
{
    public string ProductName { get; set; } = "";
    public string ProductDescription { get; set; } = "";
    public string ProductPrice { get; set; } = "";
    public int ProductCategoryId { get; set; }
    public virtual ProductCategory ProductCategory { get; set; } = default;
	public int ProductBrandId { get; set; }
    public virtual ProductBrand ProductBrand { get; set; } = default;
    public string? SalesId { get; set; } 
    public virtual Sales Sales { get; set; } = default;
}
