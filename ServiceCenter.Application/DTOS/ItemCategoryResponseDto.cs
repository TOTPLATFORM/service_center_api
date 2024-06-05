using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
public class ItemCategoryResponseDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = "";
    public  ICollection<ItemResponseDto> Items { get; set; } = new HashSet<ItemResponseDto>();
    public  ICollection<InventoryResponseDto> Inventories { get; set; } = new HashSet<InventoryResponseDto>();
}
