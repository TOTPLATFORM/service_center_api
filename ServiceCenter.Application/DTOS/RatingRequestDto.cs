using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class RatingRequestDto
{
    [Required]
    [Range(1, 5)]
    public int RatingValue { get; set; }
    [Required]
    public string CustomerId { get; set; } 
    public int ProductId { get; set; }
    public int ServiceId { get; set; }

}
