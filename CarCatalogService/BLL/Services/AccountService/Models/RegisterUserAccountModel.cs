using AutoMapper;
using CarCatalogService.DAL.Entities;

namespace CarCatalogService.BLL.Services.AccountService.Models;

/// <summary>
///     Represents a model for registering a new user account information.
/// </summary>
public class RegisterUserAccountModel
{
    /// <summary>
    ///     Gets or sets the username for the new user account.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string UserName { get; set; }

    /// <summary>
    ///     Gets or sets the password for the new user account.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Password { get; set; }
}

/// <summary>
///     AutoMapper profile for mapping <see cref="RegisterUserAccountModel"/> entities to <see cref="User"/> objects.
/// </summary>
public class RegisterUserAccountModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RegisterUserAccountModelProfile"/> class.
    /// </summary>
    public RegisterUserAccountModelProfile()
    {
        CreateMap<RegisterUserAccountModel, User>();
    }
}
