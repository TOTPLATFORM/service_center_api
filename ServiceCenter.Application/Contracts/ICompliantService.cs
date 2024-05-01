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

}
