using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class RecruitmentMapping
{
    public static void AddRecruitmentMapping(this MappingProfiles map)
    {
        map.CreateMap<RecruitmentRecordRequestDto, RecruitmentRecord>();
        map.CreateMap<RecruitmentRecord, RecruitmentRecordResponseDto>();
    }
}
