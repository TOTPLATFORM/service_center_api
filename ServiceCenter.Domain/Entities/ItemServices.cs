using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class ItemServices:AuditableEntity
{
    public virtual Service Service { get; set; }
    public int ServiceId { get; set; }
    public int ItemId { get; set; }
    public virtual Item Item { get; set; }
    public int QuantityItem { get; set; }
}
