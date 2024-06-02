using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Contact : ApplicationUser
{
    //public string ContactFirstName { get; set; } = "";
    //public string ContactLastName { get; set; } = "";
    //public string ContactEmail { get; set; } = "";
    //public string Gender { get; set; } = "";
    public Address Address { get; set; } = default;
    public ContactStatus Status { get; set; } = ContactStatus.Opportunity;
    public virtual ICollection<Complaint> Complaints { get; set; } = new HashSet<Complaint>();
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
    public virtual Report Report  { get; set; }
    public ICollection<ContactRating> CustomerRatings { get; set; }

}
