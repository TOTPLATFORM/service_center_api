using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class BranchResponseDto
{
    public int Id { get; set; }
    public string BranchName { get; set; } = "";
    public City City { get; set; } 
    public Country Country { get; set; } = Country.Egypt;
    public string PostalCode { get; set; } = "";
    public string BranchPhoneNumber { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public  ICollection<ComplaintResponseDto?> Complaints { get; set; } = new HashSet<ComplaintResponseDto?>();
    public  ManagerResponseDto? Manager { get; set; } = default;
    public  InventoryResponseDto? Inventory { get; set; } = default;



}
