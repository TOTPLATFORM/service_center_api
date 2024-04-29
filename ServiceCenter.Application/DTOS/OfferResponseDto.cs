using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class OfferResponseDto
    {
        public int Id { get; set; }
        public string OfferName { get; set; } = "";
        public string OfferDescription { get; set; } = "";
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int Discount { get; set; }
    }
}
