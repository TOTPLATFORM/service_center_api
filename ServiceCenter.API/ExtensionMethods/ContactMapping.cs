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
			.ReverseMap();

		map.CreateMap<CustomerRequestDto, Contact>();
	}
}
