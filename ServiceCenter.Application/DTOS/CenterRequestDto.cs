using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class CenterRequestDto
{
	[Required]
	public string CenterName { get; set; } = "";

	[Required]
	[Range(1, 10)]
	public int OpeningHours { get; set; }

	[Required]
	public string Specialty { get; set; } = "";
}
