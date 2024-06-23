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
    public virtual ICollection<Contact> Contact { get; set; }=new HashSet<Contact>();
    public virtual ICollection<Service?> Services { get; set; } = new HashSet<Service?>();
    public virtual ICollection<Product?> Products { get; set; } = new HashSet<Product?>();
}
