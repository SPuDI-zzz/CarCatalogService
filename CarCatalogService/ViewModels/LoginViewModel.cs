using AutoMapper;
using CarCatalogService.Services.AccountService.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Login")]
    [Required(ErrorMessage = "Login is required")]
    public required string UserName { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}

public class LoginViewModelPofile : Profile
{
    public LoginViewModelPofile()
    {
        CreateMap<LoginViewModel, LoginUserAccountModel>();
    }
}
