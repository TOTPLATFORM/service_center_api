﻿using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceProviderResponseDto:EmployeeResponseDto
{
    public string Id { get; set; } = "";
    
}
