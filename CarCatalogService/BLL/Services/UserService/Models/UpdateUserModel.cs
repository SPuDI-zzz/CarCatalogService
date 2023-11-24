using AutoMapper;
using CarCatalogService.DAL.Entities;

namespace CarCatalogService.BLL.Services.UserService.Models;

/// <summary>
///     Represents a model for updating an existing user.
/// </summary>
public class UpdateUserModel
{
    /// <summary>
    ///     Gets or sets the login name for the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Login { get; set; }

    /// <summary>
    ///     Gets or sets the roles assigned for the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required IEnumerable<string> Roles { get; set; }
}

/// <summary>
///     AutoMapper profile for mapping <see cref="UpdateUserModel"/> entities to <see cref="User"/> objects.
/// </summary>
public class UpdateUserModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UpdateUserModelProfile"/> class.
    /// </summary>
    public UpdateUserModelProfile()
    {
        CreateMap<UpdateUserModel, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Login));
    }
}
