using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ProductMapping
{
    public static void AddProductMapping(this MappingProfiles map)
    {
        map.CreateMap<ProductRequestDto, Product>()
          .ForMember(dest => dest.Sales.Id, src => src.MapFrom(src => src.SalesId))
         .ReverseMap();
        map.CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.SalesName, src => src.MapFrom(src => src.Sales.FirstName))
            .ReverseMap();
    }
}
