using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ReportMapping
{
    public static void AddReportMapping(this MappingProfiles map)
    {
        map.CreateMap<ReportRequestDto, Report>();

        map.CreateMap<Report, ReportResponseDto>()
            .ForMember(dest=> dest.SalesName, src => src.MapFrom(src => src.Sales.FirstName + " " + src.Sales.LastName));
    }
}
