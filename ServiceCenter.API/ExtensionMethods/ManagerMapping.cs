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
            .ForMember(dest => dest.BranchName, src => src.MapFrom(src => src.Branch.BranchName))
            .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName));

        map.CreateMap<Manager, ManagerGetByIdResponseDto>()
           .ForMember(dest => dest.BranchName, src => src.MapFrom(src => src.Branch.BranchName))
           .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.DepartmentName))
           .ForMember(dest => dest.Country, src => src.MapFrom(src => src.Address.Country))
           .ForMember(dest => dest.City, src => src.MapFrom(src => src.Address.City))
           .ForMember(dest => dest.PostalCode, src => src.MapFrom(src => src.Address.PostalCode)); 
	}
}
