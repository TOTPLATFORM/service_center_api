using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class OfferRequestDto
{
    [Required]
    public string OfferName { get; set; } = "";
    [Required]
    public string OfferDescription { get; set; } = "";
    [Required]
    public DateOnly StartDate { get; set; }
    [Required]
    public DateOnly EndDate { get; set; }
    [Required]
    [Range(10, 90)]
    public int Discount { get; set; }
    [Required]
    public int ProductId { get; set; }
}
