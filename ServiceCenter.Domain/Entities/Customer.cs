using Microsoft.AspNetCore.Identity;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public  class Customer:ApplicationUser
{
    public string WhatsAppNumber { get; set; } = "";
    public Address Address { get; set; } = default;
    public ContactStatus Status { get; set; } = ContactStatus.Customer;
    public virtual ICollection<Complaint?> Complaints { get; set; } = new HashSet<Complaint?>();
    public virtual ICollection<Feedback?> Feedbacks { get; set; } = new HashSet<Feedback?>();
    public virtual ICollection<Rating?> Ratings { get; set; } = new HashSet<Rating?>();
    public virtual ICollection<Subscription?> Subscriptions { get; set; } = new HashSet<Subscription?>();
}
