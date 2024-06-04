using ServiceCenter.Core.Result;
using Castle.Core.Logging;
using ServiceCenter.Application.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Application.DTOS;
using Microsoft.VisualBasic;
using AutoMapper;
using System.Data;
using ServiceCenter.Core.JWT;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using ServiceCenter.Domain.Enums;

namespace ServiceCenter.Application.Services;

public class AuthService(UserManager<ApplicationUser> userManager, ILogger<ApplicationUser> logger, IMapper mapper, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager = null) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<ApplicationUser> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly JWT _jwt = jwt.Value;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    /// <inheritdoc/>
    public async Task<Result<LoginResponseDto>> Login(LoginRequestDto LoginRequestDto)
    {
        var user = await _userManager.FindByNameAsync(LoginRequestDto.UserName);

        if (user is null || !await _userManager.CheckPasswordAsync(user, LoginRequestDto.Password))
        {
            _logger.LogWarning($"Invalid credentials entered for {LoginRequestDto.UserName}");
            return Result.Error("Invalid credentials");
        }

        var roles = (List<string>)await _userManager.GetRolesAsync(user);

        var token = GenerateJwtToken(user, roles);

        var loginResponseDto = _mapper.Map<LoginResponseDto>(user);
        loginResponseDto.Roles = roles;
        loginResponseDto.Token = token;

        _logger.LogInformation($"Successfully generated claims for {user.UserName}");
        _logger.LogInformation($"{user.UserName} successfully logged in");
        return Result<LoginResponseDto>.Success(loginResponseDto);
    }

    /// <inheritdoc/>
    public async Task<Result<ApplicationUser>> RegisterAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = FormatErrorMessage(result);

            _logger.LogError($"An error occured while creating user: {errors}");
            return Result.Error(errors);
        }

        _logger.LogInformation($"Successfully registered a new user with username {user.UserName}");
        return Result.Success(user);
    }

    /// <inheritdoc/>
    public async Task<Result> UpdateUserAsync(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = FormatErrorMessage(result);

            _logger.LogError($"An error occured while updating user with username:{user.UserName}, errors: {errors}");
            return Result.Error(errors);
        }

        _logger.LogInformation($"Successfully update user with username {user.UserName}");
        return Result.SuccessWithMessage("Successfully updated the user.");
    }

    /// <inheritdoc/>
    public async Task<Result> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequest)
    {
        var user = await _userManager.FindByIdAsync(changePasswordRequest.UserId);

        if (user is null)
        {
            _logger.LogError($"Unable to find user with id {changePasswordRequest.UserId}");
            return Result.NotFound(["The user is not found"]);
        }

        var result = await _userManager.ChangePasswordAsync(user, changePasswordRequest.CurrentPassword, changePasswordRequest.NewPassword);

        if (!result.Succeeded)
        {
            var errors = FormatErrorMessage(result);

            _logger.LogError($"An error occured while change the password for the user with username:{user.UserName}, errors: {errors}");
            return Result.Error(errors);
        }

        _logger.LogInformation($"Successfully changed password for the user with username {user.UserName}");
        return Result.SuccessWithMessage("Successfully changed user password.");
    }

    /// <inheritdoc/>
    public async Task<Result> AddUserToRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            _logger.LogError($"Unable to find user with id {userId} to assign {roleName} role");
            return Result.NotFound(["The user is not found"]);
        }

        var result = await _userManager.AddToRoleAsync(user, roleName);

        if (!result.Succeeded)
        {
            _logger.LogError($"Invalid role name {roleName} while assigning {user.UserName} to the role");
            return Result.Error(["Invalid role name"]);
        }

        _logger.LogInformation($"Successfully assigned {user.UserName} to role {roleName}");
        return Result.SuccessWithMessage($"Successfully assigned {user.FirstName + " " + user.LastName} to role {roleName}");
    }

    /// <inheritdoc/>
    public async Task<Result> RegisterUserWithRoleAsync(ApplicationUser user, string password, string role)
    {
        var registerResult = await RegisterAsync(user, password);

        if (!registerResult.IsSuccess)
        {
            return Result.Error(registerResult.Errors.SingleOrDefault());
        }

        var addRoleResult = await AddUserToRoleAsync(registerResult.Value.Id, role);

        if (!addRoleResult.IsSuccess)
        {
            return addRoleResult;
        }

        return Result.SuccessWithMessage($"Successfully created a new user {registerResult.Value.UserName} with role {role}");
    }

    /// <inheritdoc/>
    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            _logger.LogError($"Unable to find user with id {userId} to delete");
            return Result.NotFound(["The user is not found"]);
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            var errors = FormatErrorMessage(result);

            _logger.LogError($"An error occured while deleting the user with username:{user.UserName}, errors: {errors}");
            return Result.Error(errors);
        }

        _logger.LogInformation($"Successfully delete user {user.UserName}");
        return Result.SuccessWithMessage($"Successfully delete {user.FirstName + " " + user.LastName}, username: {user.UserName}");
    }

    /// <inheritdoc/>
    public async Task CreateRoles(List<string> roles)
    {
        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = role });
            }
        }
    }

    /// <inheritdoc/>
    public async Task CreateAdminAccount()
    {
        var user = new ApplicationUser
        {
            FirstName = "AdminName",
            DateOfBirth = DateOnly.MaxValue,
            Email = "admin123@gmail.com",
            Gender = Gender.Male,
            UserName = "admin",
            PhoneNumber = "01140812059",
        };

        await _userManager.CreateAsync(user, "Admin123!");

        await _userManager.AddToRoleAsync(user, "Admin");
    }

    /// <summary>
    /// Generates claims for a user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="roles">The roles.</param>
    /// <returns>The claims identity.</returns>
    public List<Claim> GenerateClaims(ApplicationUser user, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.FirstName+" "+user.LastName ),
            new Claim(ClaimTypes.Email, user.Email)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    /// <summary>
    /// generates jwt token for a specific user
    /// </summary>
    /// <param name="user">the user to generate jwt token for</param>
    /// <param name="roles">the user's roles</param>
    /// <returns>the jwt security token</returns>
    public string GenerateJwtToken(ApplicationUser user, List<string> roles)
    {
        var claims = GenerateClaims(user, roles);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
        issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays),
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }

    /// <summary>
    /// Formats the error messages from an <see cref="IdentityResult"/> into a single string.
    /// </summary>
    /// <param name="result">The <see cref="IdentityResult"/> containing the errors to format.</param>
    /// <returns>A string containing all error descriptions separated by commas.</returns>
    private string FormatErrorMessage(IdentityResult result)
    {
        var errors = string.Empty;

        foreach (var error in result.Errors)
        {
            errors += $"{error.Description},";
        }

        return errors;
    }

    public async Task<Result<BaseUserResponseDto>> GetUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            _logger.LogError($"Unable to find user with id {userId} to delete");
            return Result.NotFound(["The user is not found"]);
        }

        var userResponseDto = _mapper.Map<BaseUserResponseDto>(user);

        _logger.LogInformation($"Successfully retrieved user {user.UserName}");
        return Result.Success(userResponseDto);
    }
}