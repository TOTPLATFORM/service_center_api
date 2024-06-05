using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class CenterResponseDto
{
    public int Id { get; set; }
	public string CenterName { get; set; } = "";
	public int OpeningHours { get; set; }
	public string Specialty { get; set; } = "";
}
