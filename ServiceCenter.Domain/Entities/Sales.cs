using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Sales : ApplicationUser
{
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    
}
