using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

	public class Vendor : ApplicationUser
	{
    public DateOnly ContractStartDate { get; set; }
    public DateOnly ContractEndDate { get; set; }
    public virtual Center Center { get; set; }
    public virtual ICollection<Transaction?> Transactions { get; set; } = new HashSet<Transaction>();
}
