using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Login")]
    [Required(ErrorMessage = "Login is required")]
    public required string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
