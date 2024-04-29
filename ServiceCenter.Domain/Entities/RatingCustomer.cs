using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class RatingCustomer  : BaseEntity
{
    public string  CustomerId { get; set; }
    public int RatingId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Rating Rating { get; set; }
}
