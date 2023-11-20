using AutoMapper;
using CarCatalogService.Data;
using CarCatalogService.Data.Entities;
using CarCatalogService.Services.AccountService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarCatalogService.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<UserRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AccountService(
        UserManager<User> userManager,
        IConfiguration configuration,
        IMapper mapper,
        RoleManager<UserRole> roleManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<UserAccountModel> Register(RegisterUserAccountModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user != null)
            throw new Exception($"A user with username {model.UserName} already exist");

        user = _mapper.Map<User>(model);

        var resultCreateUser = await _userManager.CreateAsync(user, model.Password);

        if (!resultCreateUser.Succeeded)
            throw new Exception($"Creating user account is wrong" +
                $"{String.Join(", ", resultCreateUser.Errors.Select(s => s.Description))}");

        if (!await _roleManager.RoleExistsAsync(model.Role))
        {
            await _roleManager.CreateAsync(new UserRole() { Name = model.Role });
        }

        var resultAddRoles = await _userManager.AddToRoleAsync(user, model.Role);
        
        if (!resultAddRoles.Succeeded)
            throw new Exception($"Creating user account is wrong" +
                $"{String.Join(", ", resultAddRoles.Errors.Select(s => s.Description))}");

        var data = _mapper.Map<UserAccountModel>(user);
        return data;
    }

    public async Task<string> Login(LoginUserAccountModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName)
            ?? throw new Exception($"There is no user with this username {model.UserName}");

        var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!checkPassword)
            throw new Exception($"Wrong password for user {user.UserName}");

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = GetToken(authClaims);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}
