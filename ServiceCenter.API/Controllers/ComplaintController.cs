using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;
public class ComplaintController(IComplaintService ComplaintService) : BaseController
{
    private readonly IComplaintService _ComplaintService = ComplaintService;

    /// <summary>
    /// action for add Complaint  action that take  Complaint dto   
    /// </summary>
    /// <param name="ComplaintDto">Complaint  dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for Complaint  added successfully.</returns>
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddComplaint(ComplaintRequestDto ComplaintDto)
    {
        return await _ComplaintService.AddComplaintAsync(ComplaintDto);
    }

}
