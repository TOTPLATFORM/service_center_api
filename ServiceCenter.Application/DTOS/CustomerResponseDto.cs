using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class CustomerResponseDto : BaseUserResponseDto
{
	public string City { get; set; } = "";
	public string Country { get; set; } = "";
	public string PostalCode { get; set; } = "";

}
