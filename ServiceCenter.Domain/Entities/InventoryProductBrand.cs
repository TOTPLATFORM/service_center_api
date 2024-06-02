using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public  class InventoryProductBrand: AuditableEntity
{
    public int InventoryId { get; set; }
    public virtual Inventory Inventory { get; set; }

    public int ProductBrandId { get; set; }
    public virtual ProductBrand ProductBrand { get; set; }
}
