using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;
public static class ContactMapping
{
	public static void AddContactMapping(this MappingProfiles map)
	{
        map.CreateMap<ContactRequestDto, Contact>();
        map.CreateMap<Contact, ContactResponseDto>()
             .ForMember(dest => dest.Country, src => src.MapFrom(src => src.Address.Country))
            .ForMember(dest => dest.PostalCode, src => src.MapFrom(src => src.Address.PostalCode))
            .ForMember(dest => dest.City, src => src.MapFrom(src => src.Address.City));

		
	}
}
