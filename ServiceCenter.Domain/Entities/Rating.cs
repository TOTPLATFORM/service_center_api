using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Rating : AuditableEntity
{
    public int RatingValue { get; set; }
   public virtual Customer Customer { get; set; } = default;
    public virtual Product? Product { get; set; } = default;
    public virtual Service? Service { get; set; } = default;
}
