using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class SubscriptionRequestDto
{
    [Required]
    public DateOnly Duration { get; set; } 
    [Required]
    public int ServicePackageId { get; set; }
    [Required]
    public string CustomerId { get; set; }
}
