using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class RatingService : BaseEntity
{
    public int RatingId { get; set; }
    public int ServiceId { get; set; }
    public virtual Service Service { get; set; }
    public virtual Rating Rating { get; set; }
}
