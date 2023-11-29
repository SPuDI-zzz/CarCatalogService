using AutoMapper;
using CarCatalogService.BLL.Services.AccountService.Models;
using CarCatalogService.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

/// <summary>
///     Represents a view model for user registration.
/// </summary>
public class RegisterViewModel : BaseViewModel
{
    /// <summary>
    ///     Gets or sets the login (username) of the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    [Display(Name = "Login")]
    [Required(ErrorMessage = "Login is required")]
    public required string UserName { get; set; }

    /// <summary>
    ///     Gets or sets the password of the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    /// <summary>
    ///     Gets or sets the confirmation password of the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password do not match")]
    public required string ConfirmPassword { get; set; }
}

/// <summary>
///     AutoMapper profile for mapping <see cref="RegisterViewModel"/> entities to <see cref="RegisterUserAccountModel"/> objects.
/// </summary>
public class RegisterViewModelPofile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RegisterViewModelPofile"/> class.
    /// </summary>
    public RegisterViewModelPofile()
    {
        CreateMap<RegisterViewModel, RegisterUserAccountModel>();
    }
}
