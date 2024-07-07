using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class OrderMapping
{
    public static void AddOrderMapping(this MappingProfiles map)
    {
        map.CreateMap<OrderRequestDto, Order>()
            .ForPath(dest => dest.Customer.Id, src => src.MapFrom(src => src.CustomerId))
            .ForPath(dest => dest.ProductOrders, src => src.MapFrom(src => src.ProductOrders))
            .ReverseMap();

        map.CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.CustomerId, src => src.MapFrom(src => src.Customer.Id))
             .ForMember(dest => dest.TotalPrice, src => src.MapFrom(src => src.TotalPrice)) 
            .AfterMap((src, dest) =>
            {
                dest.TotalPrice = src.ProductOrders.Sum(po => po.Quantity * po.Product.ProductPrice);
            })
            ;


    }
}
