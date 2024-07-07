using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Contact 
{
    public Guid Id { get; set; } 
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public Address Address { get; set; } = default;
    public ContactStatus Status { get; set; } = ContactStatus.Opportunity;
	public string Email { get; set; } = "";
	public string WhatsAppNumber { get; set; } = "";


}
