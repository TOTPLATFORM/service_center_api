using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Product : AuditableEntity
{
    public string ProductName { get; set; } = "";
    public string ProductDescription { get; set; } = "";
    public int ProductStock { get; set; }
    public int ProductPrice { get; set; } 
    public virtual ICollection<Feedback?> Feedbacks { get; set; } = new HashSet<Feedback?>();
    public virtual ProductCategory ProductCategory { get; set; } = default;
    public virtual ICollection<ProductOrder?> ProductOrders   { get; set; } = new HashSet<ProductOrder?>();
    public virtual ICollection<Rating?> Ratings { get; set; } = new HashSet<Rating?>();
}
