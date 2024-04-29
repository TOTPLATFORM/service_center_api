using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
public class ItemResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int Stock { get; set; }
    public int Price { get; set; }
    public virtual ItemCategoryResponseDto Category { get; set; }
}
