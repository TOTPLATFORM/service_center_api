using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Transaction: AuditableEntity
{
    public int InventoryId { get; set; }
    public virtual Inventory Inventory { get; set; } = default;
    public string VendorId { get; set; } = default;
    public virtual Vendor Vendor  { get; set; } = default;

}
