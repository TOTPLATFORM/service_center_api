using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ReportMapping
{
    public static void AddReportMapping(this MappingProfiles map)
    {
        map.CreateMap<ReportRequestDto, Report>()
            .ForMember(dest => dest.Manager.Id, src => src.MapFrom(src => src.ManagerId))
            .ForMember(dest => dest.Contact.Id, src => src.MapFrom(src => src.ContactId))
            .ForMember(dest => dest.Sales.Id, src => src.MapFrom(src => src.SalesId))
            .ReverseMap();

        map.CreateMap<Report, ReportResponseDto>()
            .ReverseMap();
;
    }
}
