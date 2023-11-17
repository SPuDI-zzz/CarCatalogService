using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Login is required")]
        public required string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare(nameof(Password), ErrorMessage = "Password do not match")]
        public required string ConfirmPassword { get; set; }
    }
}
