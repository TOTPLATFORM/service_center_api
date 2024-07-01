using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.API.ExtensionMethods;

public static class AuthMapping
{
	public static void AddAuthMapping(this MappingProfiles map)
	{
		map.CreateMap<ApplicationUser, LoginResponseDto>()
			.ForMember(dest => dest.UserId, src => src.MapFrom(src => src.Id));

	}
}


