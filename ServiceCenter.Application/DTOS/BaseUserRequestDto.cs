using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class BaseUserRequestDto
{
	[Required]
	public string UserName { get; set; } = "";
    [PasswordPropertyText]
	[Required]
	public string Password { get; set; }
	[Required]
	[Phone]
    public string PhoneNumber { get; set; }
	[EmailAddress]
    public string Email { get; set; }
}