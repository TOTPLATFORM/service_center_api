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
			.ForMember(dest => dest.CustomerFirstName, src => src.MapFrom(src => src.FirstName))
			.ForMember(dest => dest.CustomerLastName, src => src.MapFrom(src => src.LastName))
			.ForMember(dest => dest.CustomerEmail, src => src.MapFrom(src => src.Email))
			.ForMember(dest => dest.CustomerPhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
			.ReverseMap();

		map.CreateMap<Customer, CustomerGetByIdResponseDto>()
			.ForMember(dest => dest.BranchName, src => src.MapFrom(src => src.Branch.BranchName))
			.ForMember(dest => dest.CustomerFirstName, src => src.MapFrom(src => src.FirstName))
			.ForMember(dest => dest.CustomerLastName, src => src.MapFrom(src => src.LastName))
			.ForMember(dest => dest.CustomerEmail, src => src.MapFrom(src => src.Email))
			.ForMember(dest => dest.CustomerPhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
			.ForMember(dest=> dest.City, src => src.MapFrom(src => src.Address.City))
			.ForMember(dest => dest.Country, src => src.MapFrom(src => src.Address.Country))
			.ForMember(dest => dest.PostalCode, src => src.MapFrom(src => src.Address.PostalCode))
			.ReverseMap();
	}
}

