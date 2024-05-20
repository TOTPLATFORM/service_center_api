using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class OverviewMapping
{
    public static void AddOverviewMapping(this MappingProfiles map)
    {
        map.CreateMap<OverviewRequestDto, Overview>();

        map.CreateMap<Overview, OverviewResponseDto>()
            .ForMember(dest=> dest.SalesName, src => src.MapFrom(src => src.Sales.FirstName + " " + src.Sales.LastName));
    }
}
