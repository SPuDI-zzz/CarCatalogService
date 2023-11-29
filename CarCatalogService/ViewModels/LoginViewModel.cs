using AutoMapper;
using CarCatalogService.BLL.Services.AccountService.Models;
using CarCatalogService.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

/// <summary>
///     Represents a view model for user login.
/// </summary>
public class LoginViewModel : BaseViewModel
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
}

/// <summary>
///     AutoMapper profile for mapping <see cref="LoginViewModel"/> entities to <see cref="LoginUserAccountModel"/> objects.
/// </summary>
public class LoginViewModelPofile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LoginViewModelPofile"/> class.
    /// </summary>
    public LoginViewModelPofile()
    {
        CreateMap<LoginViewModel, LoginUserAccountModel>();
    }
}
