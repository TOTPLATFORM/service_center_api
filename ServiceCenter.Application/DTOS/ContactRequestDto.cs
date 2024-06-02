using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ContactRequestDto
{
	[Required]
	public string ContactFirstName { get; set; } = "";
	[Required]
	public string ContactLastName { get; set; } = "";
	[Required]
	[EmailAddress]
	public string ContactEmail { get; set; } = "";
	[Required]
	public string Gender { get; set; } = "";
	public Address Address { get; set; } = default;

}
