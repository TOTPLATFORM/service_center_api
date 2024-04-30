using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class CustomerService : BaseEntity
{
    public string CustomerId { get; set; }
    public int ServiceId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Service Service { get; set; }
}
