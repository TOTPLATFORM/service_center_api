using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ProductCategoryResponseDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = "";
    public  ICollection<ProductGetByIdResponseDto> Products { get; set; } = new HashSet<ProductGetByIdResponseDto>();
    public  ICollection<ProductBrandResponseDto> ProductBrands { get; set; } = new HashSet<ProductBrandResponseDto>();
}
