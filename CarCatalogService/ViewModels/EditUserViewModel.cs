using AutoMapper;
using CarCatalogService.BLL.Services.UserService.Models;
using CarCatalogService.Shared.Enum;
using CarCatalogService.ViewModels.Common;

namespace CarCatalogService.ViewModels;

/// <summary>
///     Represents a view model for editing an existing user entity.
/// </summary>
public class EditUserViewModel : BaseViewModel
{
    /// <summary>
    ///     Gets or sets the identifier of the user entity to edit.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     Gets or sets the login of the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Login { get; set; }

    /// <summary>
    ///     Gets or sets the roles assigned to the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required IEnumerable<RolesEnum> Roles { get; set; }
}


/// <summary>
///     AutoMapper profile for mapping <see cref="UserModel"/> entities to <see cref="EditUserViewModel"/>
///     and <see cref="EditUserViewModel"/> entities to <see cref="UpdateUserModel"/> classes.
/// </summary>
public class EditUserViewModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EditUserViewModelProfile"/> class.
    /// </summary>
    public EditUserViewModelProfile()
    {
        CreateMap<UserModel, EditUserViewModel>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(role => (RolesEnum)Enum.Parse(typeof(RolesEnum), role))));
        CreateMap<EditUserViewModel, UpdateUserModel>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(role => role.ToString())));
    }
}
