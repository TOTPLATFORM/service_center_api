using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class WareHouseManagerRequestDto
{
    [Required]
    public string WareHouseManagerEmail { get; set; } = "";
    [Required]
    public string WareHouseManagerFirstName { get; set; } = "";
    [Required]
    public string WareHouseManagerLastName { get; set; } = "";
    [Required]
    public string WareHouseManagerPhoneNumber { get; set; } = "";
    [Required]
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; } = "";
    [Required]
    public string PositionTitle { get; set; } = "";
    [Required]
    public DateOnly StartDate { get; set; }
    [Required]
    public DateOnly EndtDate { get; set; }
    [Required]
    public int InventoryId { get; set; }
}
