using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
public class ItemCategoryResponeDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = "";
    public int ReferenceNumber { get; set; }
}
