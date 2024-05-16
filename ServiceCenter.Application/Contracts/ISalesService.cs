using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface ISalesService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add sales that take salesDto   
    /// </summary>
    /// <param name="salesRequestDto">sales request dto</param>
    /// <returns>Sales added successfully </returns>
    public Task<Result> AddSalesAsync(SalesRequestDto salesRequestDto);

    /// <summary>
    /// function to get all sales 
    /// </summary>
    /// <returns>list all salesResponseDto </returns>
    public Task<Result<List<SalesResponseDto>>> GetAllSalesAsync();

    /// <summary>
    /// function to get sales by id that take  sales id
    /// </summary>
    /// <param name="id">sales id</param>
    /// <returns>sales response dto</returns>
    public Task<Result<SalesResponseDto>> GetSalesByIdAsync(string id);

    /// <summary>
    /// function to update sales that take SalesRequestDto   
    /// </summary>
    /// <param name="id">sales id</param>
    /// <param name="salesRequestDto">sales dto</param>
    /// <returns>Updated Sales </returns>
    public Task<Result<SalesResponseDto>> UpdateSalesAsync(string id, SalesRequestDto salesRequestDto);


    /// <summary>
    /// function to search sales by text  that take text   
    /// </summary>
    /// <param name="text">text</param>
    /// <returns>all saleses that contain this text </returns>
    public Task<Result<List<SalesResponseDto>>> SearchSalesByTextAsync(string text);

    /// <summary>
    /// function to delete Sales that take SalesDto   
    /// </summary>
    /// <param name="id">departmnet id</param>
    /// <returns>Sales removed successfully </returns>
    public Task<Result> DeleteSalesAsync(string id);
}
