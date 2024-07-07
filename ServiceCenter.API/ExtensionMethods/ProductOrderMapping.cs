using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static  class ProductOrderMapping
{
    public static void AddProductOrderMapping(this MappingProfiles map)
    {
        map.CreateMap<ProductOrderRequestDto,ProductOrder>()
            .ReverseMap();

        map.CreateMap<ProductOrder,ProductOrderResponseDto>()
            .ReverseMap();



    }
}