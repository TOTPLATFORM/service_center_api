using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class RatingResponseDto
{
    public int Id { get; set; }
    public int RatingValue { get; set; }
    public DateTime RatingDate { get; set; }
    public string  ContactName { get; set; }
    public ProductResponseDto? Product { get; set; } = default;
    public ServiceResponseDto? Service { get; set; } = default;
}
