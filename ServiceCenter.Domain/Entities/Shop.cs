using Microsoft.AspNetCore.Http.HttpResults;
using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Shop : AuditableEntity
{
    public string ShopName { get; set; } = "";
    public string ShopPhoneNumber { get; set; } = "";
    public DateOnly CreatedDate { get; set; }
	public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

}
