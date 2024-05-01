using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class FeedbackController(IFeedbackService FeedbackService) : BaseController
{
    private readonly IFeedbackService _FeedbackService = FeedbackService;

    /// <summary>
    /// action for add Feedback  action that take  Feedback dto   
    /// </summary>
    /// <param name="FeedbackDto">Feedback  dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for Feedback  added successfully.</returns>
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddFeedback(FeedbackRequestDto FeedbackDto)
    {
        return await _FeedbackService.AddFeedbackAsync(FeedbackDto);
    }
    /// <summary>
    /// get all Feedback categories in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<FeedbackResponseDto>>> GetAllFeedback()
    {
        return await _FeedbackService.GetAllFeedbackAsync();
    }
    /// <summary>
    /// get Feedback by id in the system.
    /// </summary>
    ///<param name="id">id of Feedback.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<FeedbackResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<FeedbackResponseDto>> GetFeedbackById(int id)
    {
        return await _FeedbackService.GetFeedbackByIdAsync(id);
    }
}
