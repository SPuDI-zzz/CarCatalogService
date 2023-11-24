using AutoMapper;
using CarCatalogService.BLL.Services.AccountService.Models;
using CarCatalogService.DAL.Entities;
using CarCatalogService.Shared.Const;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarCatalogService.BLL.Services.AccountService;

/// <summary>
///     Service for user authentication
/// </summary>
public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AccountService" /> class.
    /// </summary>
    /// <param name="userManager">The user manager for register </param>
    /// <param name="configuration">For configuring</param>
    /// <param name="mapper">To mapping an object of one type to another</param>
    public AccountService(
        UserManager<User> userManager,
        IConfiguration configuration,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
    }

    /// <inheritdoc />
    /// <exception cref="Exception">
    ///     Thrown when the registration process encounters an error, such as an existing username,
    ///     an issue creating the user account.
    /// </exception>
    public async Task RegisterAsync(RegisterUserAccountModel model)
    {
        var user = _mapper.Map<User>(model);

        var resultCreateUser = await _userManager.CreateAsync(user, model.Password);

        if (!resultCreateUser.Succeeded)
            throw new Exception($"Creating user account is wrong" +
                $"{string.Join(", ", resultCreateUser.Errors.Select(s => s.Description))}");

        await _userManager.AddToRoleAsync(user, AppRoles.User);
    }

    /// <inheritdoc />
    /// <exception cref="Exception">
    ///     Thrown when the login process encounters an error, such as an invalid username,
    ///     wrong password.
    /// </exception>
    public async Task<string> LoginAsync(LoginUserAccountModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName)
            ?? throw new Exception($"There is no user with this username {model.UserName}");

        var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!checkPassword)
            throw new Exception($"Wrong password for user {user.UserName}");

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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

    /// <summary>
    ///     Generates a JWT (JSON Web Token) with the specified authentication claims.
    /// </summary>
    /// <param name="authClaims">
    ///     A list of <see cref="Claim"/> objects representing the user's authentication claims.
    /// </param>
    /// <returns>
    ///     A <see cref="JwtSecurityToken"/> representing the generated JWT with the provided
    ///     authentication claims.
    /// </returns>
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
