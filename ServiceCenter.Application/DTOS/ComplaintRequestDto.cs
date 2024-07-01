using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ComplaintRequestDto
{
   [Required]
    public string ComplaintDescription { get; set; } = "";
    [Required]
    public string CustomerId { get; set; } = "";
    public Status ComplaintStatus { get; set; }=Status.Pending;
    public string ServiceProviderId { get; set; } = "";
    public int BranchId { get; set; }
}
