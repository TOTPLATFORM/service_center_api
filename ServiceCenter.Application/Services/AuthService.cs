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

namespace ServiceCenter.Application.Services;

public class AuthService(UserManager<ApplicationUser> userManager, ILogger<ApplicationUser> logger, IMapper mapper, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager = null) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<ApplicationUser> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly JWT _jwt = jwt.Value;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    /// <summary>
    /// Logs a user in.
    /// </summary>
    /// <param name="LoginRequestDto">The login model.</param>
    /// <returns>The result of the login attempt.</returns>
    //public async Task<Result<LoginResponseDto>> Login(LoginRequestDto LoginRequestDto)
    //{
    //    var user = await _userManager.FindByNameAsync(LoginRequestDto.UserName);

    //    if (user is null || !await _userManager.CheckPasswordAsync(user, LoginRequestDto.Password))
    //    {
    //        _logger.LogWarning($"Invalid credentials entered for {LoginRequestDto.UserName}");
    //        return Result.Error("Invalid credentials");
    //    }

    //    var roles = (List<string>)await _userManager.GetRolesAsync(user);

    //    var token = GenerateJwtToken(user, roles);

    //    var loginResponseDto = _mapper.Map<LoginResponseDto>(user);
    //    loginResponseDto.Roles = roles;
    //    loginResponseDto.Token = token;

    //    _logger.LogInformation($"Successfully generated claims for {user.UserName}");
    //    _logger.LogInformation($"{user.UserName} successfully logged in");
    //    return Result<LoginResponseDto>.Success(loginResponseDto);
    //}

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="user">The user to register.</param>
    /// <param name="password">The user's entered password.</param>
    /// <returns>The result of the registration attempt.</returns>
    public async Task<Result<ApplicationUser>> RegisterAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = string.Empty;

            foreach (var error in result.Errors)
            {
                errors += $"{error.Description},";
            }

            _logger.LogError($"An error occured while creating user: {errors}");
            return Result.Error(errors);
        }

        _logger.LogInformation($"Successfully registered a new user with username {user.UserName}");
        return Result.Success(user);
    }

    /// <summary>
    /// Adds a user to specific role.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="roleName">The role name.</param>
    /// <returns>The result of trying to add a user to role.</returns>
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
        return Result.SuccessWithMessage($"Successfully assigned {user.FirstName} {user.LastName} to role {roleName}");
    }

    /// <summary>
    /// Registers a user and assign role.
    /// </summary>
    /// <param name="user">The user to register.</param>
    /// <param name="password">The user's entered password.</param>
    /// <param name="role">The role to assig for the user</param>
    /// <returns>The result of the registration attempt.</returns>
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

	/// <summary>
	/// Registers a new customer with customer role.
	/// </summary>
	/// <param name="customerRequestDto">The Dto of the customer.</param>
	/// <returns>The result of the registration attempt.</returns>
	public async Task<Result> RegisterCustomerAsync(CustomerRequestDto customerRequestDto)
    {
        string role = "Customer";

        var customer = _mapper.Map<Customer>(customerRequestDto);

        return await RegisterUserWithRoleAsync(customer, customerRequestDto.Password, role);
    }

    /// <summary>
    /// Registers a new doctor with doctor role.
    /// </summary>
    /// <param name="doctorDto">The Dto of the doctor.</param>
    /// <returns>The result of the registration attempt.</returns>
    //public async Task<Result> RegisterDoctorAsync(DoctorRequestDto doctorDto)
    //{
    //    string role = "Doctor";

    //    var doctor = _mapper.Map<Doctor>(doctorDto);

    //    return await RegisterUserWithRoleAsync(doctor, doctorDto.Password, role);
    //}

    /// <summary>
    /// Registers a new laboratorist with laboratorist role.
    /// </summary>
    /// <param name="laboratoristDto">The Dto of the laboratorist.</param>
    /// <returns>The result of the registration attempt.</returns>
    //public async Task<Result> RegisterLaboratoristAsync(LaboratoriestRequestDto laboratoristDto)
    //{
    //    string role = "Laboratorist";

    //    var laboratorist = _mapper.Map<Laboratorist>(laboratoristDto);

    //    return await RegisterUserWithRoleAsync(laboratorist, laboratoristDto.Password, role);
    //}

    /// <summary>
    /// Registers a new employee with employee role.
    /// </summary>
    /// <param name="employeeDto">The Dto of the employee.</param>
    /// <returns>The result of the registration attempt.</returns>
    public async Task<Result> RegisterEmployeeAsync(EmployeeRequestDto employeeDto)
    {
        string role = "Employee";

        var employee = _mapper.Map<Employee>(employeeDto);

        return await RegisterUserWithRoleAsync(employee, employeeDto.Password, role);
    }

    /// <summary>
    /// Registers a new sales with sales role.
    /// </summary>
    /// <param name="salesDto">The Dto of the sales.</param>
    /// <returns>The result of the registration attempt.</returns>
    public async Task<Result> RegisterSalesAsync(SalesRequestDto salesDto)
    {
        string role = "Employee";

        var sales = _mapper.Map<Sales>(salesDto);

        return await RegisterUserWithRoleAsync(sales, salesDto.Password, role);
    }

    /// <summary>
    /// Registers a new pharmacist with pharmacist role.
    /// </summary>
    /// <param name="pharmacistDto">The Dto of the pharmacist.</param>
    /// <returns>The result of the registration attempt.</returns>
    //public async Task<Result> RegisterPharmacistAsync(PharmacistRequestDto pharmacistDto)
    //{
    //    string role = "Pharmacist";

    //    var pharmacist = _mapper.Map<Pharmacist>(pharmacistDto);

    //    return await RegisterUserWithRoleAsync(pharmacist, pharmacistDto.Password, role);
    //}

    /// <summary>
    /// Registers a new nurse with nurse role.
    /// </summary>
    /// <param name="nurseDto">The Dto of the nurse.</param>
    /// <returns>The result of the registration attempt.</returns>
    //public async Task<Result> RegisterNurseAsync(NurseRequestDto nurseDto)
    //{
    //    string role = "Nurse";

    //    var nurse = _mapper.Map<Nurse>(nurseDto);

    //    return await RegisterUserWithRoleAsync(nurse, nurseDto.Password, role);
    //}

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
            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
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

    public async Task InitializeRoles()
    {
        var roles = new List<string> {"Manager", "Employee","Customer","Sales"};

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = role });
            }
        }
    }

    public async Task CreateManagerAccount()
    {
        var user = new ApplicationUser
        {
            FirstName = "Service",
            LastName = "Center",
            DateOfBirth = DateOnly.MaxValue,
            Email = "center133@gmail.com",
            Gender = "Male",
            UserName = "manager"
        };

        await _userManager.CreateAsync(user, "P@ssw0rd");

        await _userManager.AddToRoleAsync(user, "Manager");
    }

}
