using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IFeedbackService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add  Feedback  that take  FeedbackDto   
    /// </summary>
    /// <param name="FeedbackRequestDto">Feedback  request dto</param>
    /// <returns> Feedback  added successfully </returns>
    public Task<Result> AddFeedbackAsync(FeedbackRequestDto FeedbackRequestDto);
    /// <summary>
    /// function to get all Feedback  
    /// </summary>
    /// <returns>list all Feedback  response dto </returns>
    public Task<Result<List<FeedbackResponseDto>>> GetAllFeedbackAsync();
}
