using AutoMapper;
using CarCatalogService.DAL.Entities;

namespace CarCatalogService.BLL.Services.UserService.Models;

/// <summary>
///     Represents a model for adding a new user.
/// </summary>
public class AddUserModel
{
    /// <summary>
    ///     Gets or sets the login name for the new user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Login { get; set; }

    /// <summary>
    ///     Gets or sets the password for the new user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Password { get; set; }

    /// <summary>
    ///     Gets or sets the roles assigned for the new user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required IEnumerable<string> Roles { get; set; }
}

/// <summary>
///     AutoMapper profile for mapping <see cref="AddUserModel"/> entities to <see cref="User"/> objects.
/// </summary>
public class AddUserModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AddUserModelProfile"/> class.
    /// </summary>
    public AddUserModelProfile()
    {
        CreateMap<AddUserModel, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Login));
    }
}
