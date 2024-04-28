using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Customer : ApplicationUser
{
    public Address Address { get; set; } 
    public int BranchId { get; set; }
    public virtual Branch Branch { get; set; }
    public  virtual ICollection<Complaint> Complaints { get; set; } = new HashSet<Complaint>();
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
    public virtual ICollection<Appotiment> Appotiments  { get; set; } = new HashSet<Appotiment>();
}
