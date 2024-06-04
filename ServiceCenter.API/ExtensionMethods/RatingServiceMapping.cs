using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class RatingServiceMapping
{
    public static void AddRatingServiceMapping(this MappingProfiles map)
    {
        map.CreateMap<RatingRequestDto, Rating>()
          .ForMember(d => d.CreatedDate, o => o.MapFrom(s => s.RatingDate)); ;
        map.CreateMap<Rating, RatingResponseDto>()
            .ForMember(d => d.RatingDate, o => o.MapFrom(s => s.CreatedDate)); ;
    
    }
}
