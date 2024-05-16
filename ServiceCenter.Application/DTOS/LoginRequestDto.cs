﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class LoginRequestDto
{
	[Required]
	public string UserName { get; set; } = "";
	[Required]
	public string Password { get; set; } = "";
}
