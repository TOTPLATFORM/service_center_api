using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;
public static class ContactMapping
{
	public static void AddContactMapping(this MappingProfiles map)
	{
        map.CreateMap<ContactRequestDto, Contact>();

        map.CreateMap<CustomerRequestDto, Contact>();
        map.CreateMap<Contact, ContactResponseDto>()
              .ForMember(dest => dest.ContactFirstName, src => src.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.ContactLastName, src => src.MapFrom(src => src.LastName))
            .ForMember(dest => dest.ContactPhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.ContactEmail, src => src.MapFrom(src => src.Email))
             .ForMember(dest => dest.Country, src => src.MapFrom(src => src.Address.Country))
            .ForMember(dest => dest.PostalCode, src => src.MapFrom(src => src.Address.PostalCode))
            .ForMember(dest => dest.City, src => src.MapFrom(src => src.Address.City))
            .ReverseMap();

		
	}
}
