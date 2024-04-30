using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ProductRequestDto
{
    [Required]
    public string ProductName { get; set; } = "";
    [Required]
    public string ProductDescription { get; set; } = "";
    [Required]
    public string ProductPrice { get; set; } = "";
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public int ProductBrandId { get; set; }
    public string? SalesId { get; set; } = "";
}
