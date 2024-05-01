using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
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

}
