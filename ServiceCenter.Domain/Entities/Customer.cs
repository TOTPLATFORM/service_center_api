using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Customer : ApplicationUser
{
    public Address Address { get; set; } = default;
    public int BranchId { get; set; }
    public virtual Branch Branch { get; set; } = default;
    public  virtual ICollection<Complaint> Complaints { get; set; } = new HashSet<Complaint>();
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
    public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
}
