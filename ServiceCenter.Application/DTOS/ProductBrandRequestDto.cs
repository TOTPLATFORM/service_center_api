using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ProductBrandRequestDto
{

    [Required]
    public string BrandName { get; set; } = "";
    [Required]
    public string BrandDescription { get; set; } = "";
    [Required]
    public string CountryOfOrigin { get; set; } = "";
    [Required]
    public DateOnly FoundedYear { get; set; }
}
