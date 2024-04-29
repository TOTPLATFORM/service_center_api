using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Feedback : AuditableEntity
{
    public DateOnly FeedbackDate { get; set; }
    public string FeedbackDescription { get; set; } = "";
    public string FeedbackCategory { get; set; } = "";
    public string? CustomerId { get; set; } 
    public virtual Customer Customer { get; set; }

}
