﻿using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ComplaintMapping
{
    public static void AddCompliantMapping(this MappingProfiles map)
    {
  
        map.CreateMap<Complaint,ComplaintRequestDto>()
           .ForPath(dest => dest.CustomerId, src => src.MapFrom(src => src.Customer.Id))
          .ForPath(dest => dest.ServiceProviderId, src => src.MapFrom(src => src.ServiceProvider.Id))
          .ForPath(dest => dest.BranchId, src => src.MapFrom(src => src.Branch.Id))
         // .ForMember(dest => dest.ComplaintStatus, opt => opt.MapFrom(src => src.ComplaintStatus))
           .ReverseMap();

        map.CreateMap<ComplaintResponseDto,Complaint>()
            .ForPath(dest => dest.Customer.FirstName, src => src.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.ComplaintDate))

            .ReverseMap();
       
    }
}
