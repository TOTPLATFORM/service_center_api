using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ProductGetByIdResponseDto : ProductResponseDto
{
    public int ProducrStock { get; set; }
    public virtual ICollection<FeedbackResponseDto?> Feedbacks { get; set; } = new HashSet<FeedbackResponseDto>();
  
    
}
