using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class WareHouseManagerResponseDto
    {
        public int Id { get; set; }
        public string WareHouseManagerEmail { get; set; } = "";
        public string WareHouseManagerFirstName { get; set; } = "";
        public string WareHouseManagerLastName { get; set; } = "";
        public string WareHouseManagerPhoneNumber { get; set; } = "";
        public string UserName { get; set; } = "";
        public string PositionTitle { get; set; } = "";
        public DateOnly StartDate { get; set; }
        public DateOnly EndtDate { get; set; }
        public string InventoryName { get; set; } = "";
    }
}
