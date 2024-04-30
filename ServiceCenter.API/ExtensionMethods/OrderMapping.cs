using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class OrderMapping
{
    public static void AddOrderMapping(this MappingProfiles map)
    {
        map.CreateMap<OrderRequestDto, Order>().ReverseMap();
        map.CreateMap<Order, OrderResponseDto>().ReverseMap();
    }
}
