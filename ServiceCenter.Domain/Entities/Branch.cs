using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Branch : AuditableEntity
{
    public string BranchName { get; set; } = "";
    public Address Address { get; set; } 
    public string BranchPhoneNumber { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public int CenterId { get; set; }
    public Center Center { get; set; }
    public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
}
