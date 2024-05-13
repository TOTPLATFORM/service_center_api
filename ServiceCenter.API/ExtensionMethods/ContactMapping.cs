using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;
public static class ContactMapping
{
	public static void AddContactMapping(this MappingProfiles map)
	{
		map.CreateMap<ContactRequestDto, Contact>().ReverseMap();

		map.CreateMap<Contact,ContactResponseDto>()
			.ForMember(dest=>dest.FirstName,src=>src.MapFrom(src=>src.ContactFirstName))
			.ForMember(dest => dest.LastName, src => src.MapFrom(src => src.ContactLastName))
			.ForMember(dest => dest.Email, src => src.MapFrom(src => src.ContactEmail))
			.ForMember(dest => dest.City, src => src.MapFrom(src => src.Address.City))
			.ForMember(dest => dest.Country, src => src.MapFrom(src => src.Address.Country))
			.ForMember(dest => dest.PostalCode, src => src.MapFrom(src => src.Address.PostalCode))
			.ReverseMap();	
	}
}
