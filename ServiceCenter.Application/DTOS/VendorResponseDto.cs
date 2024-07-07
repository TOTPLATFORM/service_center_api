using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class VendorResponseDto:BaseUserResponseDto
{
    public string UserName { get; set; } = "";
    public DateOnly ContractStartDate { get; set; }
    public DateOnly ContractEndDate { get; set; }

}
