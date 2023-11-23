using AutoMapper;
using CarCatalogService.BLL.Services.AccountService.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "Login")]
    [Required(ErrorMessage = "Login is required")]
    public required string UserName { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password do not match")]
    public required string ConfirmPassword { get; set; }
}

public class RegisterViewModelPofile : Profile
{
    public RegisterViewModelPofile()
    {
        CreateMap<RegisterViewModel, RegisterUserAccountModel>();
    }
}
