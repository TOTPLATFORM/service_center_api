using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class BaseUserRequestDto
{
	[Required]
	public string FirstName { get; set; } = "";
	[Required]
	public string LastName { get; set; } = "";
	[Required]
	public DateOnly DateOfBirth { get; set; }
	[Required]
	public string Gender { get; set; } = "";
	[Required]
	public string Phone { get; set; } = "";
	[Required]
	public string Email { get; set; } = "";
	public string? UserName { get; set; }
	public string? Password { get; set; }
}
