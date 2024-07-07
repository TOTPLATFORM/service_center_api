using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class BaseUserResponseDto
{
    public string Id { get; set; } = "";
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Role { get; set; } = "";
}