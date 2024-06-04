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
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// action for login a user that take login request dto.
    /// </summary>
    /// <param name="loginModel">The login model.</param>
    /// <returns>result representing of the login successfully.</returns>
    [HttpPost("Login")]
    [ProducesResponseType(typeof(Result<LoginResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<LoginResponseDto>> Login(LoginRequestDto loginModel)
    {
        var result = await _authService.Login(loginModel);

        if (!result.IsSuccess)
        {
            return result;
        }

        return result;
    }

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
    public async Task<Result> AddUserToRole(string userId, string roleName)
    {
        return await _authService.AddUserToRoleAsync(userId, roleName);
    }
    /// <summary>
    /// action for add a staff roles.
    /// </summary>
    /// <returns>result representing the adding the staff roles successfully.</returns>
    [HttpPost("CreateRoles")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> CreateRoles(List<string> roles)
    {
        await _authService.CreateRoles(roles);

        return Result.SuccessWithMessage("Create roles successfully");
    }

    /// <summary>
    /// action for add an admin account.
    /// </summary>
    /// <returns>result representing the adding admin successfully.</returns>
    [HttpPost("CreateAdminAccount")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> CreateAdminAccount()
    {
        await _authService.CreateAdminAccount();

        return Result.SuccessWithMessage("Create admin account successfully");
    }

    /// <summary>
    /// Deletes a specified user.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <returns>A result representing the attempt to delete the user.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteUser/{userId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> DeleteUser(string userId)
    {
        return await _authService.DeleteUserAsync(userId);
    }

    /// <summary>
    /// Changes the password for a specified user.
    /// </summary>
    /// <param name="changePasswordRequest">The request dto containing information to change the password.</param>
    /// <returns>A result representing the attempt to change the password.</returns>
    [Authorize]
    [HttpPut("ChangePassword")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> ChangePassword(ChangePasswordRequestDto changePasswordRequest)
    {
        return await _authService.ChangePasswordAsync(changePasswordRequest);
    }

    /// <summary>
    /// Get the current logged in user data.
    /// </summary>
    /// <returns>A result containing the current logged in user data.</returns>
    [Authorize]
    [HttpGet("GetCurrentUser")]
    [ProducesResponseType(typeof(Result<BaseUserResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<BaseUserResponseDto>>> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _authService.GetUserAsync(userId);
    }
}
