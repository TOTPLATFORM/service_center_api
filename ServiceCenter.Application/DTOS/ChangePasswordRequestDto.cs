using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ChangePasswordRequestDto
{
	[Required]
	public string UserId { get; set; } = "";

	[Required]
	public string CurrentPassword { get; set; } = "";

	[Required]
	public string NewPassword { get; set; } = "";
}
