using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class SalesResponseDto
{
    public string Id { get; set; } = "";
    public EmployeeResponseDto Employee { get; set; } = default;
}
