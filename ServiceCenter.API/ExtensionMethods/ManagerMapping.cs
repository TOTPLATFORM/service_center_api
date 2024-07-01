using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ManagerMapping
{
	public static void AddManagerMapping(this MappingProfiles map)
	{
		map.CreateMap<ManagerRequestDto, Manager>();

		map.CreateMap<Manager, ManagerResponseDto>()
		   .ForMember(dest => dest.ManagerEmail, src => src.MapFrom(src => src.Email))
		   .ForMember(dest => dest.ManagerPhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
		   .ForMember(dest=>dest.DepartmentName,src=>src.MapFrom(src=>src.Department.DepartmentName))
		   .ReverseMap();

		map.CreateMap<Manager, ManagerGetByIdResponseDto>()
		   .ForMember(dest => dest.ManagerEmail, src => src.MapFrom(src => src.Email))
		   .ForMember(dest => dest.ManagerPhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
		   .ForMember(dest=>dest.DepartmentName,src=>src.MapFrom(src=>src.Department.DepartmentName));
	}
}
