using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class FeedbackRequestDto
{
    [Required]
    public string FeedbackDescription { get; set; } = "";
   
    [Required]
    public string ContactId { get; set; } = "";
    public int ProductId  { get; set; }
    public int ServiceId { get; set; }
}
