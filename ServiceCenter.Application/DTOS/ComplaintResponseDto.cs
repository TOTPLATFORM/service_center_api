using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ComplaintResponseDto
{
    public int Id { get; set; }
    public DateTime ComplaintDate { get; set; }
    public string ComplaintDescription { get; set; } = "";
    public Status ComplaintStatus { get; set; } 
    public string ContactName { get; set; } = "";
    public ServiceProviderResponseDto? ServiceProvider { get; set; } = default;
    public BranchResponseDto? Branch { get; set; } = default;
 

}
