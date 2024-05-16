using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Overviews;

public interface IOverviewService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add Overview that take OverviewDto   
    /// </summary>
    /// <param name="OverviewRequestDto">Overview request dto</param>
    /// <returns> Overview  added successfully </returns>
    public Task<Result> AddOverviewAsync(OverviewRequestDto OverviewRequestDto);

    /// <summary>
    /// function to get all Overview  
    /// </summary>
    /// <returns>list all Overview  response dto </returns>
    public Task<Result<List<OverviewResponseDto>>> GetAllOverviewAsync();
    /// <summary>
    /// function to get  Overview  by id that take   Overview id
    /// </summary>
    /// <param name="id"> Overview  id</param>
    /// <returns> Overview  response dto</returns>
    public Task<Result<OverviewResponseDto>> GetOverviewByIdAsync(int id);

    /// <summary>
    /// function to update Overview  that take OverviewRequestDto   
    /// </summary>
    /// <param name="id">Overview id</param>
    /// <param name="OverviewRequestDto">Overview dto</param>
    /// <returns>Updated Overview </returns>
    public Task<Result<OverviewResponseDto>> UpdateOverviewAsync(int id, OverviewRequestDto OverviewRequestDto);
    /// <summary>
    /// function to delete Overview  that take Overview  id   
    /// </summary>
    /// <param name="id">Overview  id</param>
    /// <returns>Overview  removed successfully </returns>
    public Task<Result> DeleteOverviewAsync(int id);
}
