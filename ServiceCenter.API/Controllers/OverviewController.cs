using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Overviews;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using Microsoft.AspNetCore.Authorization;

namespace ServiceCenter.API.Controllers;

public class OverviewController(IOverviewService OverviewService) : BaseController
{
    private readonly IOverviewService _OverviewService = OverviewService;

    /// <summary>
    /// action for add Overview action that take Overview dto   
    /// </summary>
    /// <param name="OverviewDto">Overview dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for Overview  added successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddOverview(OverviewRequestDto OverviewDto)
    {
        return await _OverviewService.AddOverviewAsync(OverviewDto);
    }


    /// <summary>
    /// get all Overview in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<List<OverviewResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<OverviewResponseDto>>> GetAllOverview()
    {
        return await _OverviewService.GetAllOverviewAsync();
    }
    /// <summary>
    /// get Overview by id in the system.
    /// </summary>
    ///<param name="id">id of Overview.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<OverviewResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<OverviewResponseDto>> GetOverviewById(int id)
    {
        return await _OverviewService.GetOverviewByIdAsync(id);
    }
    /// </summary>
    ///<param name="id">id of Overview.</param>
    ///<param name="OverviewRequestDto">Overview dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<OverviewResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<OverviewResponseDto>> UpdateOverview(int id, OverviewRequestDto OverviewRequestDto)
    {
        return await _OverviewService.UpdateOverviewAsync(id, OverviewRequestDto);
    }
    /// <summary>
    /// delete  Overview  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteOverviewAsycn(int id)
    {
        return await _OverviewService.DeleteOverviewAsync(id);
    }

}
