using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ProductBrandResponseDto
{
    public int Id { get; set; }
    public string BrandName { get; set; } = "";
    public string BrandDescription { get; set; } = "";
    public Country CountryOfOrigin { get; set; } 
    public DateOnly FoundedYear { get; set; }

    public  ICollection<ProductCategoryResponseDto> ProductCategories { get; set; } = new HashSet<ProductCategoryResponseDto>();
    public  ICollection<InventoryResponseDto> Inventories { get; set; } = new HashSet<InventoryResponseDto>();
}
