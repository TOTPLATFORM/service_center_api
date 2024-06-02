using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class ProductBrand : AuditableEntity
{
    public string BrandName { get; set; } = "";
    public string BrandDescription { get; set; } = "";
    public string CountryOfOrigin { get; set; } = "";
    public DateOnly FoundedYear { get; set; }
    public virtual ICollection<ProductCategory> ProductCategories { get; set; }=new HashSet<ProductCategory>();
    public virtual ICollection<Inventory> Inventories { get; set; } = new HashSet<Inventory>();

}
