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
    public DateOnly RatingDate { get; set; }

}
