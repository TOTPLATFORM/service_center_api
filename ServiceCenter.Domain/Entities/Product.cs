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
   public virtual ICollection<Feedback?> Feedbacks { get; set; } = new HashSet<Feedback>();

   public virtual Sales? Sales { get; set; } = default;

}
