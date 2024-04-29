﻿using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class ProductBrand : AuditableEntity
{
    public string BrandName { get; set; } = "";
    public string BrandDescription { get; set; } = "";
    public string CountryOfOrigin { get; set; } = "";
    public DateOnly FoundedYear { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

}
