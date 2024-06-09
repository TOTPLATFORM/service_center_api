using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceResponseDto
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = "";
    public string ServiceDescription { get; set; } = "";
    public int ServicePrice { get; set; }
    public Status Avaliable { get; set; }
	public ICollection<ServicePackageResponseDto> ServicePackages { get; set; } = new HashSet<ServicePackageResponseDto>();
    public  CenterResponseDto Center { get; set; } = default;
    public  ServiceCategoryResponseDto ServiceCategory { get; set; } = default;
   // public string ServiceProviderName { get; set; } = "";
    public ICollection<string> ServiceProviderId { get; set; } = new HashSet<string>();
    public  ICollection<ItemResponseDto> Item { get; set; } = new HashSet<ItemResponseDto>();
    public  ICollection<FeedbackResponseDto?> Feedbacks { get; set; } = new HashSet<FeedbackResponseDto?>();

}
