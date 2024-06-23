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
  
    public ICollection< ProductResponseDto?> Product { get; set; } = new HashSet<ProductResponseDto>();
    public ICollection<ServiceResponseDto?> Service { get; set; } = new HashSet<ServiceResponseDto>();
}
