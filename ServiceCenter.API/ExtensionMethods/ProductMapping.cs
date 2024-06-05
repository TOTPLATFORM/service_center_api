using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ProductMapping
{
    public static void AddProductMapping(this MappingProfiles map)
    {
        map.CreateMap<ProductRequestDto, Product>()
         .ReverseMap();
        map.CreateMap<Product, ProductResponseDto>()
               
            .ReverseMap();
    }
}
