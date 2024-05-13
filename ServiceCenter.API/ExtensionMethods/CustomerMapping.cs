using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class CustomerMapping
{
	public static void AddCustomerMapping(this MappingProfiles map)
	{
		map.CreateMap<CustomerRequestDto, Customer>()
			.ForMember(dest=>dest.FirstName,src=>src.MapFrom(src=>src.CustomerFirstName))
			.ForMember(dest => dest.LastName, src => src.MapFrom(src => src.CustomerLastName))
			.ForMember(dest => dest.Email, src => src.MapFrom(src => src.CustomerEmail))
			.ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.CustomerPhoneNumber))
			.ReverseMap();

		map.CreateMap<Customer, CustomerResponseDto>()
			.ReverseMap();
	}
}

