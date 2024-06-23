using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class RatingMapping
{
    public static void AddRatingServiceMapping(this MappingProfiles map)
    {
        //map.CreateMap<RatingRequestDto, Rating>();
        //map.CreateMap<Rating, RatingResponseDto>()
        // .ForMember(dest => dest.RatingDate, opt => opt.MapFrom(src => src.CreatedDate))
        // .ForMember(d => d.ContactName, o => o.MapFrom(s => s.Contact.Select(i => i.FirstName)));
        //map.CreateMap<RatingResponseDto, Rating>()
        //   .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.RatingDate))
        //   .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => new HashSet<Contact>
        //   {
        //        new Contact { FirstName = src.ContactName }
        //   }));
        map.CreateMap<RatingRequestDto, Rating>();
          

        map.CreateMap<Rating, RatingResponseDto>()
          .ForMember(dest => dest.RatingDate, opt => opt.MapFrom(src => src.CreatedDate));

    }
}
