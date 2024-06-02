using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class ContactRating:AuditableEntity
{
    public int ContactId { get; set; }
    public Contact Contact  { get; set; }

    public int RatingId { get; set; }
    public Rating Rating { get; set; }
}
