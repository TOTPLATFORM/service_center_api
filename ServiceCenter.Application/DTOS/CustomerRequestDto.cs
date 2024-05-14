using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.DTOS;

public class CustomerRequestDto
{
	[Required]
	[EmailAddress]
	public string CustomerEmail { get; set; } = "";
	[Required]
	public string CustomerFirstName { get; set; } = "";
	public string? CustomerLastName { get; set; }
	[Required]
	[Phone]
	public string CustomerPhoneNumber { get; set; } = "";
	[Required]
	public string UserName { get; set; } = "";
	[PasswordPropertyText]
	public string Password { get; set; } = "";
	public DateOnly DateOfBirth { get; set; }
	public string Gender { get; set; } = "";
	public Address Address { get; set; } = default;
    public int BranchId { get; set; } 
}
