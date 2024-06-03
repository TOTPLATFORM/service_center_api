using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ProductCategoryRequestDto
{
    [Required]
    public string CategoryName { get; set; } = "";
    
    [Required]
    public List<int> ProductBrandsId { get; set; }
}
