using AutoMapper;
using CarCatalogService.DAL.Entities;
using CarCatalogService.BLL.Services.AccountService;
using CarCatalogService.BLL.Services.AccountService.Models;
using CarCatalogService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers;

/// <summary>
///     Controller responsible for handling user authentication and account-related actions.
/// </summary>
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="accountService">The service responsible for user account operations.</param>
    /// <param name="mapper">The AutoMapper instance for mapping between models.</param>
    /// <param name="signInManager">The SignInManager for handling user sign-in.</param>
    /// <param name="userManager">The UserManager for managing user-related operations.</param>
    public AccountController(IAccountService accountService, IMapper mapper, UserManager<User> userManager)
    {
        _accountService = accountService;
        _mapper = mapper;
        _userManager = userManager;
    }

    /// <summary>
    ///     Displays the login view.
    /// </summary>
    /// <returns>The login view.</returns>
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    ///     Handles the POST request for user login.
    /// </summary>
    /// <param name="loginViewModel">The view model containing user login information.</param>
    /// <returns>
    ///     If the login is successful, it sets a token cookie and redirects to the home page;
    ///     otherwise, it returns the login view with an error message or a <see cref="BadRequestResult"/>.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var loginModel = _mapper.Map<LoginUserAccountModel>(loginViewModel);

        var responseModel = await _accountService.LoginAsync(loginModel);
        if (responseModel.IsError)
        {
            TempData["Error"] = "Wrong credentials. Please try again.";
            return View(loginViewModel);
        }

        HttpContext.Response.Cookies.Append("token", responseModel.Token,
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddHours(3),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
            });

        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    ///     Displays the registration view.
    /// </summary>
    /// <returns>The registration view.</returns>
    public IActionResult Register()
    {
        return View();
    }

    /// <summary>
    ///     Handles the POST request for user registration.
    /// </summary>
    /// <param name="registerViewModel">The view model containing user registration information.</param>
    /// <returns>
    ///     Redirects to the login page on successful registration; otherwise, 
    ///     returns the registration view with an error message or a <see cref="BadRequestResult"/>.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) 
            return BadRequest();

        var registerModel = _mapper.Map<RegisterUserAccountModel>(registerViewModel);

        var responseModel = await _accountService.RegisterAsync(registerModel);
        if (responseModel.IsError)
        {
            TempData["Error"] = responseModel.ErrorMessage;
            return View(registerViewModel);
        }
        
        return RedirectToAction(nameof(Login));
    }

    /// <summary>
    ///     Logs out the user by deleting the authentication token cookie.
    /// </summary>
    /// <returns>Redirects to the login page.</returns>
    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("token");
        return RedirectToAction("Login", "Account");
    }
}
