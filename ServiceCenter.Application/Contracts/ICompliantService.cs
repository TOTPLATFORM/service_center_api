using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IComplaintService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add  Complaint  that take  ComplaintDto   
    /// </summary>
    /// <param name="ComplaintRequestDto">Complaint  request dto</param>
    /// <returns> Complaint  added successfully </returns>
    public Task<Result> AddComplaintAsync(ComplaintRequestDto ComplaintRequestDto);

    /// <summary>
    /// function to get all Complaint  
    /// </summary>
    /// <returns>list all Complaint  response dto </returns>
    public Task<Result<List<ComplaintResponseDto>>> GetAllComplaintsAsync();

    /// <summary>
    /// function to get Complaint by id that take Complaint id
    /// </summary>
    /// <param name="id"> Complaint id</param>
    /// <returns> Complaint  response dto</returns>
    public Task<Result<ComplaintResponseDto>> GetComplaintByIdAsync(int id);

    /// <summary>
    /// function to update Complaint that take ComplaintRequestDto   
    /// </summary>
    /// <param name="id">Complaint id</param>
    /// <param name="ComplaintRequestDto">Complaint dto</param>
    /// <returns>Updated Complaint </returns>
    public Task<Result<ComplaintResponseDto>> UpdateComplaintAsync(int id, ComplaintRequestDto ComplaintRequestDto);

    /// <summary>
    /// function to delete Complaint  that take Complaint  id   
    /// </summary>
    /// <param name="id">Complaint  id</param>
    /// <returns>Complaint  removed successfully </returns>
    public Task<Result> DeleteComplaintAsync(int id);

    /// <summary>
    /// function to search by Complaint status  that take  Complaint status
    /// </summary>
    /// <param name="text">Complaint status</param>
    /// <returns>Complaint response dto </returns>
    public Task<Result<List<ComplaintResponseDto>>> SearchComplaintByTextAsync(Status text);
}
