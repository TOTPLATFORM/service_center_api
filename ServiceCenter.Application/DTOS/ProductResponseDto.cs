using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ProductResponseDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = "";
    public string ProductDescription { get; set; } = "";
    public string ProductPrice { get; set; } = "";
    public string CategoryName { get; set; } = "";
    public string ProductBrandName { get; set; } = "";
    public string? SalesName { get; set; } = "";
    
}
