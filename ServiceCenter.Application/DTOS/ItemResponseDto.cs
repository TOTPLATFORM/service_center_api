using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
public class ItemResponseDto
{
 public int Id { get; set; }
public string ItemName { get; set; } = "";
public string ItemDescription { get; set; } = "";
public int ItemStock { get; set; }
public int ItemPrice { get; set; }
public  ItemCategoryResponseDto Category { get; set; }
 }
