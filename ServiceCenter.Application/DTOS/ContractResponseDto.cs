using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ContractResponseDto
{
    public int Id { get; set; }
    public DateOnly Duration { get; set; } 
    public string PackageName { get; set; } = "";
}
