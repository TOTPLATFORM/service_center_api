using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.Domain.Enums;

namespace ServiceCenter.Application.DTOS;

public class BaseUserUpdateRequestDto
{
	public string FirstName { get; set; } = "";
	public string LastName { get; set; } = "";
	public DateOnly DateOfBirth { get; set; }
	public Gender Gender { get; set; } 
	[Phone]
	public string Phone { get; set; } = "";
	[EmailAddress]
	public string Email { get; set; } = "";
}
