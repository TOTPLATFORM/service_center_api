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
    public DateOnly ComplaintDate { get; set; }
    public string ComplaintDescription { get; set; } = "";
    public ComplaintType ComplaintCategory { get; set; }
    public Status ComplaintStatus { get; set; }
    
}
