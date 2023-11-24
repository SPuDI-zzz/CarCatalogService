using AutoMapper;
using CarCatalogService.DAL.Entities;

namespace CarCatalogService.BLL.Services.AccountService.Models;

/// <summary>
///     Represents a model for authenticating a user account.
/// </summary>
public class LoginUserAccountModel
{
    /// <summary>
    ///     Gets or sets the username associated with the user account for authentication.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string UserName { get; set; }

    /// <summary>
    ///     Gets or sets the password associated with the user account for authentication.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Password { get; set; }
}

/// <summary>
///     AutoMapper profile for mapping <see cref="LoginUserAccountModel"/> entities to <see cref="User"/> objects.
/// </summary>
public class LoginUserAccountModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LoginUserAccountModelProfile"/> class.
    /// </summary>
    public LoginUserAccountModelProfile()
    {
        CreateMap<LoginUserAccountModel, User>();
    }
}
