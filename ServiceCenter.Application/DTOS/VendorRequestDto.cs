using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class VendorRequestDto : BaseUserRequestDto
{
    [Required]
    public int ContactId { get; set; }
    [Required]
    public DateOnly ContractStartDate { get; set; }
    [Required]
    public DateOnly ContractEndDate { get; set; }
  
}
