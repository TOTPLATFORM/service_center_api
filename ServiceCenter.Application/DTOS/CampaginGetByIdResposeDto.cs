﻿using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class CampaginGetByIdResposeDto : CampaginResponseDto
{
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }
	public int Budget { get; set; }
	public string Goals { get; set; } = "";
	public CampaginStatus Status { get; set; } 
}
