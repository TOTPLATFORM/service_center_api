using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class CustomerOffer : BaseEntity
{
    public string CustomerId { get; set; }
    public int OfferId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Offer Offer { get; set; }
}
