using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities
{
	public class Vendor : ApplicationUser
	{
		public string VendorName { get; set; } = "";
		public string VendorType { get; set; } = "";
        public string ContactPerson { get; set; } = "";
        public DateOnly ContractStartDate { get; set; }
        public DateOnly ContractEndDate { get; set; }
        public int CenterId { get; set; }
        public virtual Center Center { get; set; } = default;

    }
}
