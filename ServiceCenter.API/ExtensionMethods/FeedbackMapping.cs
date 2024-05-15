using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static  class FeedbackMapping
{
    public static void AddFeedbackMapping(this MappingProfiles map)
    {
        map.CreateMap<FeedbackRequestDto, Feedback>().ReverseMap();
        map.CreateMap<Feedback, FeedbackResponseDto>()
            .ReverseMap();
    }
}
