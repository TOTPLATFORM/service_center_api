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
            .ForMember(dest=>dest.TotalPrice,src=>src.MapFrom(src=>src.Quantity*src.Product.ProductPrice))
            .ReverseMap();



    }
}