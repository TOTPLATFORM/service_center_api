using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class RatingServiceGetByIdResponseDto
{
    public int Id { get; set; }
    public int RatingValue { get; set; }
    public DateTime CreatedDate { get; set; }
    public  ServiceGetByIdResponseDto Service { get; set; } = default;
   // public  CustomerGetByIdResponseDto Customer { get; set; } = default;
}
