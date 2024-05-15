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
    /// <summary>
    /// function to get  Feedback  by id that take   Feedback id
    /// </summary>
    /// <param name="id"> Feedback  id</param>
    /// <returns> Feedback  response dto</returns>
    public Task<Result<FeedbackResponseDto>> GetFeedbackByIdAsync(int id);
    /// <summary>
    /// function to update Feedback  that take FeedbackRequestDto   
    /// </summary>
    /// <param name="id">Feedback id</param>
    /// <param name="FeedbackRequestDto">Feedback dto</param>
    /// <returns>Updated Feedback </returns>
    public Task<Result<FeedbackResponseDto>> UpdateFeedbackAsync(int id, FeedbackRequestDto FeedbackRequestDto);
    /// <summary>
    /// function to delete Feedback  that take Feedback  id   
    /// </summary>
    /// <param name="id">Feedback  id</param>
    /// <returns>Feedback  removed successfully </returns>
    public Task<Result> DeleteFeedbackAsync(int id);
    /// function to search by customer name  that take  Feedbacks
    /// </summary>
    /// <param name="text">customer name </param>
    /// <returns>Feedback response dto </returns>
    public Task<Result<List<FeedbackResponseDto>>> GetFeedbacksByCustomerAsync(string customerId);
}
