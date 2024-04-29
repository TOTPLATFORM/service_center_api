using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class RatingResponseDto
    {
        public int Id { get; set; }
        public int RatingValue { get; set; }
        public DateOnly RatingDate { get; set; }

    }
}
