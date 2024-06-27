using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Order : AuditableEntity
{
    public virtual  Contact Contact { get; set; }
    public Status OrderStatus { get; set; }
    public virtual ICollection<ItemOrder> ItemOrders { get; set; } = new HashSet<ItemOrder>();

}
