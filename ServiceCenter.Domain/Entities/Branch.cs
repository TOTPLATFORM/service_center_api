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
    public Address Address { get; set; } = default;
    public string BranchPhoneNumber { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public virtual Center Center { get; set; } = default;
    public virtual ICollection<Complaint?> Complaints { get; set; } = new HashSet<Complaint>();
    public virtual  Manager Manager { get; set; } = default;
    public virtual Inventory? Inventory { get; set; } = default;
}
