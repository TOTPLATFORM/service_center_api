using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ProductMapping
{
    public static void AddProductMapping(this MappingProfiles map)
    {
        map.CreateMap<ProductRequestDto, Product>()
            .ForPath(dest => dest.ProductCategory.Id, src => src.MapFrom(src => src.ProductCategoryId));

        map.CreateMap<Product, ProductGetByIdResponseDto>();

        map.CreateMap<Product, ProductResponseDto>();
        
    }
}
