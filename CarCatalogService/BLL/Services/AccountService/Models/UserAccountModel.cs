using AutoMapper;
using CarCatalogService.DAL.Entities;

namespace CarCatalogService.BLL.Services.AccountService.Models;

/// <summary>
///     Represents a model for user account information.
/// </summary>
public class UserAccountModel
{
    /// <summary>
    ///     Gets or sets the unique identifier of the user account.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     Gets or sets the username associated with the user account.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string UserName { get; set; }
}

/// <summary>
///     AutoMapper profile for mapping <see cref="User"/> entities to <see cref="UserAccountModel"/> objects.
/// </summary>
public class UserAccountModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserAccountModelProfile"/> class.
    /// </summary>
    public UserAccountModelProfile()
    {
        CreateMap<User, UserAccountModel>();
    }
}
