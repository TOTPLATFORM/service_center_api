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
    public string? ContactId { get; set; } = "";
    public virtual Contact Contact { get; set; } = default;
    public int? ServiceId { get; set; }
    public virtual Service Service { get; set; } = default;

    public int? ProductId { get; set; }
    public virtual Product Product { get; set; } = default;
}
