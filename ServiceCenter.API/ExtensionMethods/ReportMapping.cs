using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ReportMapping
{
    public static void AddReportMapping(this MappingProfiles map)
    {
        map.CreateMap<ReportRequestDto, Report>()
            .ForPath(dest => dest.Manager.Id, src => src.MapFrom(src => src.ManagerId))
            .ForPath(dest => dest.Contact.Id, src => src.MapFrom(src => src.ContactId))
            .ForPath(dest => dest.Sales.Id, src => src.MapFrom(src => src.SalesId))
            .ReverseMap();

        map.CreateMap<Report, ReportResponseDto>()
             .ForPath(dest => dest.ContactStatus, src => src.MapFrom(src => src.Contact.Status))
            .ReverseMap();
;
    }
}
