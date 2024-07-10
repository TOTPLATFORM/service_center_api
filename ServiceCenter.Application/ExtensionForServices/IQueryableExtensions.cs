using Microsoft.EntityFrameworkCore;
using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.ExtensionForServices;

public static class IQueryableExtensions
{
	public static async Task<PaginationResult<T>> GetAllWithPagination<T>(this IQueryable<T> query, int itemCount, int index)
	{
		var totalCount = await query.CountAsync();
		itemCount = (itemCount <= 0 || itemCount > totalCount) ? totalCount : itemCount;

		index = (index < 0 || index >= totalCount) ? 0 : index;

		var endIndex = index + itemCount;
		endIndex = (endIndex > totalCount) ? totalCount : endIndex;

		var result = new PaginationResult<T>
		{
			TotalCount = totalCount,
			Data = await query.Skip(index).Take(itemCount).ToListAsync(),
			PageCount = (int)Math.Ceiling(totalCount / (double)itemCount),
			HasNextPage = index + itemCount < totalCount,
			HasPreviousPage = index > 0,
			Start = index + 1,
			End = endIndex
		};
		return result;
	}
}
