using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
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
}
