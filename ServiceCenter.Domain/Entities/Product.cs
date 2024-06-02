﻿using ServiceCenter.Core.Entities;
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
    public int ProductPrice { get; set; } 
    public int ProductCategoryId { get; set; }
    public virtual ProductCategory ProductCategory { get; set; } = default;
    public virtual ICollection<Feedback?> Feedbacks { get; set; } = new HashSet<Feedback>();

    //public string? SalesId { get; set; } = default;
    //public virtual Sales Sales { get; set; } = default;

}
