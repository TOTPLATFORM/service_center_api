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
    public int RatingValue { get; set; }
    [Required]
    public DateOnly RatingDate { get; set; }

}
