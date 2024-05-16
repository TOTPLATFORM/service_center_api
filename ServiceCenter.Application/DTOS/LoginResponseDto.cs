using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class LoginResponseDto
{
    public string UserId { get; set; }
    public string UserName { get; set; } = "";
	public string FullName { get; set; } = "";
	public required List<string> Roles { get; set; }
	public string Token { get; set; } = "";
}