using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ApplicantMapping
{
    public static void AddApplicantMapping(this MappingProfiles map) 
    {
        map.CreateMap<ApplicantRequestDto, Applicant>().ReverseMap();
        map.CreateMap<Applicant, ApplicantResponseDto>().ReverseMap();


    }
}
