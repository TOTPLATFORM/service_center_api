using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class RatingServiceResponseDto
{    public int Id { get; set; }
    public int RatingValue { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ServiceName { get; set; } = "";
    public string CustomerName { get; set; } = "";

}
