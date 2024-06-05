﻿using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ManagerMapping
{
	public static void AddManagerMapping(this MappingProfiles map)
	{
		map.CreateMap<ManagerRequestDto, Manager>();

		map.CreateMap<Manager,ManagerResponseDto>()
			.ForMember(dest=>dest.DepartmentName,src=>src.MapFrom(src=>src.Department.DepartmentName));
	}
}
