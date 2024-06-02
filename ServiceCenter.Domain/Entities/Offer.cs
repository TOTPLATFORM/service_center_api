using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Offer: AuditableEntity
{
    public string OfferName { get; set; } = "";
    public string OfferDescription { get; set; } = "";
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int Discount { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = default;
    public int ServiceId { get; set; }
    public virtual Service Service { get; set; } = default;
}
