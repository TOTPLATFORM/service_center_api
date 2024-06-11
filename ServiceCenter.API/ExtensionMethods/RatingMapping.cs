using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class RatingMapping
{
    public static void AddRatingServiceMapping(this MappingProfiles map)
    {
        map.CreateMap<RatingRequestDto, Rating>();
        map.CreateMap<Rating, RatingResponseDto>()
               .ForMember(d => d.ContactName,o=>o.MapFrom(s=>s.Contact.SelectMany(c=>c.FirstName)))
               .ForMember(d => d.RatingDate, o => o.MapFrom(s => s.CreatedDate)); 

    }
}
