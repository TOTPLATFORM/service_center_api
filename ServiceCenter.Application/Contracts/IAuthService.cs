using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ServiceCenter.Application.Contracts;
/// <summary>
/// provides an interface for auth-related services that manages auth data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IAuthService : IApplicationService, IScopedService
{
	/// <summary>
	/// Logs a user in using the provided login model.
	/// </summary>
	/// <param name="loginRequestDto">The login model containing user credentials.</param>
	/// <returns>A task representing the asynchronous operation, with a result containing login response dto as the login attempt result.</returns>
	Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);

	/// <summary>
	/// Registers a new user with the specified user details and password.
	/// </summary>
	/// <param name="user">The user to register.</param>
	/// <param name="password">The password for the new user.</param>
	/// <returns>A task representing the asynchronous operation, with a result containing application user> as the registration attempt result.</returns>
	Task<Result<ApplicationUser>> RegisterAsync(ApplicationUser user, string password);

	/// <summary>
	/// Registers a user with the specified user details, password, and role.
	/// </summary>
	/// <param name="user">The user to register.</param>
	/// <param name="password">The password for the new user.</param>
	/// <param name="role">The role to assign to the new user.</param>
	/// <returns>A task representing the asynchronous operation, with a result as the registration attempt result.</returns>
	Task<Result> RegisterUserWithRoleAsync(ApplicationUser user, string password, string role);

	/// <summary>
	/// Updates the details of an existing user.
	/// </summary>
	/// <param name="user">The user with updated details.</param>
	/// <returns>A task representing the asynchronous operation, with a result as the update attempt result.</returns>
	Task<Result> UpdateUserAsync(ApplicationUser user);

	/// <summary>
	/// Get user data.
	/// </summary>
	/// <param name="userId">The user id to retrieve its data.</param>
	/// <returns>A task representing the asynchronous operation, with a result as the update attempt result.</returns>
	Task<Result<BaseUserResponseDto>> GetUserAsync(string userId);

	/// <summary>
	/// Changes the password for a specified user.
	/// </summary>
	/// <param name="userId">The ID of the user whose password is to be changed.</param>
	/// <param name="currentPassword">The current password of the user.</param>
	/// <param name="newPassword">The new password for the user.</param>
	/// <returns>A task representing the asynchronous operation, with a result as the change password attempt result.</returns>
	Task<Result> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequest);

	/// <summary>
	/// Deletes a specified user.
	/// </summary>
	/// <param name="userId">The ID of the user to delete.</param>
	/// <returns>A task representing the asynchronous operation, with a result as the delete attempt result.</returns>
	Task<Result> DeleteUserAsync(string userId);

	/// <summary>
	/// Adds a user to a specified role.
	/// </summary>
	/// <param name="userId">The ID of the user to be added to the role.</param>
	/// <param name="roleName">The name of the role to add the user to.</param>
	/// <returns>A task representing the asynchronous operation, with a result as the add user to role attempt result.</returns>
	Task<Result> AddUserToRoleAsync(string userId, string roleName);

	/// <summary>
	/// Creates the specified roles in the system.
	/// </summary>
	/// <param name="roles">A list of role names to be created.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	Task CreateRoles(List<string> roles);

	/// <summary>
	/// Creates an administrator account with predefined settings.
	/// </summary>
	/// <returns>A task representing the asynchronous operation.</returns>
	Task CreateAdminAccount();
}
