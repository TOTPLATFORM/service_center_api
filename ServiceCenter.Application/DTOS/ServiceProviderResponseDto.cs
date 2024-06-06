using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceProviderResponseDto
{
    public string Id { get; set; } = "";
    public string ServiceProviderEmail { get; set; } = "";
    public string ServiceProviderFirstName { get; set; } = "";
    public string ServiceProviderLastName { get; set; } = "";
    public string ServiceProviderPhoneNumber { get; set; } = "";
    public Gender Gender { get; set; }
    public string UserName { get; set; } = "";
}
