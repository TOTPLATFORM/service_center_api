using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class CustomerOffer : BaseEntity
{
    public int CustomerId { get; set; }
    public int OfferId { get; set; }
    public Customer Customer { get; set; }
    public Offer Offer { get; set; }
}
