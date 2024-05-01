using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ServiceCenter.API.Controllers;
public class AuthController(IAuthService authService) : BaseController
{
    private readonly IAuthService _authService= authService;

    /// <summary>
    /// action for registration a new patient that take patient request dto.
    /// </summary>
    /// <param name="registerModel">The registration model.</param>
    /// <returns>result representing of the registration successfully.</returns>
    //[HttpPost("RegisterPatient")]
    //[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    //public async Task<Result> RegisterAsync(PatientRequestDto patientDto)
    //{

    //    var result = await _authService.RegisterPatientAsync(patientDto);

    //    return result;
    //}

    /// <summary>
    /// action for login a user that take login request dto.
    /// </summary>
    /// <param name="loginModel">The login model.</param>
    /// <returns>result representing of the login successfully.</returns>
    //[HttpPost("Login")]
    //[ProducesResponseType(typeof(Result<LoginResponseDto>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    //public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto loginModel)
    //{
    //    var result = await _authService.Login(loginModel);

    //    if (!result.IsSuccess)
    //    {
    //        return result;
    //    }

    //    return result;
    //}

    /// <summary>
    /// action add a user to a specific role.
    /// </summary>
    /// <param name="userId">the user id.</param>
    /// <param name="roleName">the role name.</param>
    /// <returns>result representing the adding the user to the role successfully.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("AddUserToRole/userId/{userId}/roleName/{roleName}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddUserToRoleAsync(string userId, string roleName)
    {
        return await _authService.AddUserToRoleAsync(userId, roleName);
    }
    /// <summary>
    /// action for add a staff roles.
    /// </summary>
    /// <returns>result representing the adding the staff roles successfully.</returns>
    [HttpPost("CreateInitialRoles")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> CreateRoles()
    {
        await _authService.InitializeRoles();

         return Result.SuccessWithMessage("Create roles successfully");
    }
    /// <summary>
    /// action for add an admin account.
    /// </summary>
    /// <returns>result representing the adding admin successfully.</returns>
    [HttpPost("CreateManagerAccount")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> CreateManagerAccount()
    {
        await _authService.CreateManagerAccount();

        return Result.SuccessWithMessage("Create manager account successfully");
    }
}

