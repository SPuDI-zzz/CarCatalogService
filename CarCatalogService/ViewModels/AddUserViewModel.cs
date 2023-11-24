using AutoMapper;
using CarCatalogService.BLL.Services.UserService.Models;
using CarCatalogService.Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

/// <summary>
///     Represents a view model for creating a new user entity.
/// </summary>
public class AddUserViewModel
{
    /// <summary>
    ///     Gets or sets the login of the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    [Required(ErrorMessage = "Login is required")]
    public required string Login { get; set; }

    /// <summary>
    ///     Gets or sets the password of the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public required string Password {  get; set; }

    /// <summary>
    ///     Gets or sets the roles assigned to the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required IEnumerable<RolesEnum> Roles { get; set; }
}

/// <summary>
///     AutoMapper profile for mapping <see cref="AddUserViewModel"/> entities to <see cref="AddUserModel"/> objects.
/// </summary>
public class AddUserViewModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AddUserViewModelProfile"/> class.
    /// </summary>
    public AddUserViewModelProfile()
    {
        CreateMap<AddUserViewModel, AddUserModel>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(role => role.ToString())));
    }
}
