using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods;

public static class ComplaintMapping
{
    public static void AddCompliantMapping(this MappingProfiles map)
    {
  
        map.CreateMap<ComplaintRequestDto, Complaint>()
           .ForPath(dest => dest.Contact.Id, src => src.MapFrom(src => src.ContactId))
          .ForPath(dest => dest.ServiceProvider.Id, src => src.MapFrom(src => src.ServiceProviderId))
          .ForPath(dest => dest.Branch.Id, src => src.MapFrom(src => src.BranchId))
          .ForMember(dest =>dest.ComplaintStatus,src=>src.MapFrom(src => src.ComplaintStatus))
           .ReverseMap();

        map.CreateMap<Complaint,ComplaintResponseDto>()
            .ForMember(dest => dest.ContactName, src => src.MapFrom(src => src.Contact.FirstName))
            .ForMember(dest => dest.ComplaintDate, opt => opt.MapFrom(src => src.CreatedDate))
             .ForMember(dest => dest.ComplaintStatus, src => src.MapFrom(src => src.ComplaintStatus))
            .ReverseMap();
       
    }
}
