using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class ProductOrder : AuditableEntity
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public virtual Product Product { get; set; }
}
