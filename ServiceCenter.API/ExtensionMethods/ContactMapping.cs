//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Domain.Entities;

//namespace ServiceCenter.API.ExtensionMethods;
//public static class ContactMapping
//{
//	public static void AddContactMapping(this MappingProfiles map)
//	{
//		map.CreateMap<ContactRequestDto, Contact>()
//			.ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.ContactFirstName))
//            .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.ContactLastName))
//            .ForMember(dest => dest.Email, src => src.MapFrom(src => src.ContactEmail))
//			.ReverseMap();

//		map.CreateMap<ContactResponseDto,Contact>()
//			.ForMember(dest=>dest.FirstName,src=>src.MapFrom(src=>src.ContactFirstName))
//			.ForMember(dest => dest.LastName, src => src.MapFrom(src => src.ContactLastName))
//			.ForMember(dest => dest.Email, src => src.MapFrom(src => src.ContactEmail))
//			.ForMember(dest => dest.Address.City, src => src.MapFrom(src => src.City))
//			.ForMember(dest => dest.Address.Country, src => src.MapFrom(src => src.Country))
//			.ForMember(dest => dest.Address.PostalCode, src => src.MapFrom(src => src.PostalCode))
//			.ReverseMap();	
//	}
//}
