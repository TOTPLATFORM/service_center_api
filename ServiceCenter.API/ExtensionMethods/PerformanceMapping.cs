using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static  class PerformanceMapping
{
    public static void AddPreformanceMapping(this MappingProfiles map)
    {
        map.CreateMap<PerformanceReviewRequestDto, PerformanceReview>();
        map.CreateMap<PerformanceReview, PerformanceReviewResponseDto>();
    }
}
