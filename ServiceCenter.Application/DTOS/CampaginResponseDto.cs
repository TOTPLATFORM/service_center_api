using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class CampaginResponseDto
{
    public int Id { get; set; }
    public string CamapginName { get; set; } = "";
	public string CampaginDescription { get; set; } = "";
	
}
