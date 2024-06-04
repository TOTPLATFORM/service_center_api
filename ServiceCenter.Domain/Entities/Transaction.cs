using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Transaction: AuditableEntity
{
    public string TransactionType { get; set; } = "";
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public Status Status { get; set; }=Status.Pending;
    public virtual Inventory Inventory { get; set; } = default;
    public virtual Vendor Vendor { get; set; } = default;

}
