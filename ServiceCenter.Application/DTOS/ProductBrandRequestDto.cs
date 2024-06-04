using ServiceCenter.Domain.Enums;
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
    public Country CountryOfOrigin { get; set; } 
    [Required]
    public DateOnly FoundedYear { get; set; }

    public ICollection<int> ProductcategoriesId { get; set; }
    [Required]
    public ICollection< int> InventoriesId { get; set; }

}
