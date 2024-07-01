using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class RevenueMapping
{
    public static void AddRevenueMapping(this MappingProfiles map)
    {
        map.CreateMap<RevenueRequestDto, Revenue>();
        map.CreateMap<Revenue, RevenueResponseDto>();
    }
}
