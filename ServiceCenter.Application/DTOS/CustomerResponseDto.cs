using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class CustomerResponseDto
{
	public string Id { get; set; } = "";
    public string CustomerEmail { get; set; } = "";
	public string CustomerFirstName { get; set; } = "";
	public string? CustomerLastName { get; set; }
	public string CustomerPhoneNumber { get; set; } = "";
	public string BranchName { get; set; } = "";

}
