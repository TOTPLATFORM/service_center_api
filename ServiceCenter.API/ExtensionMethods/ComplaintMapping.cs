using ServiceCenter.API.Mapping;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.API.ExtensionMethods
{
    public static class ComplaintMapping
    {
        public static void AddCompliantMapping(this MappingProfiles map)
        {
            map.CreateMap<ComplaintRequestDto, Complaint>().ReverseMap();
            map.CreateMap<Complaint,ComplaintResponseDto>()
                .ForMember(dest=>dest.CustomerName , src => src.MapFrom(src=>src.Customer.FirstName))
                .ReverseMap();

        
        }
    }
}
