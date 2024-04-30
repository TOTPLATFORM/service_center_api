using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ProductBrandMapping
{
    public static void AddProductBrandMapping(this MappingProfiles map)
    {
        map.CreateMap<ProductBrandRequestDto, ProductBrand>().ReverseMap();
        map.CreateMap<ProductBrand, ProductBrandResponseDto>().ReverseMap();
    }
}
