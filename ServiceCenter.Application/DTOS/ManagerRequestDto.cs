using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class ManagerRequestDto
    {
        [Required]
        public string ManagerEmail { get; set; } = "";
        [Required]
        public string ManagerFirstName { get; set; } = "";
        [Required]
        public string ManagerLastName { get; set; } = "";
        [Required]
        public string ManagerPhoneNumber { get; set; } = "";
        [Required]
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = "";
        public string Responsibilities { get; set; } = "";
        [Required]
        public DateOnly HiringDate { get; set; }
        [Required]
        public int WorkingHours { get; set; }
        [Required]
        public int Experience { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}
