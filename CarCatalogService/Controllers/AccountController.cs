using AutoMapper;
using CarCatalogService.Data.Entities;
using CarCatalogService.Services.AccountService;
using CarCatalogService.Services.AccountService.Models;
using CarCatalogService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    public IActionResult Login()
    {
        return View();
    }

    // TODO
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
            return View(loginViewModel);

        var loginModel = _mapper.Map<LoginUserAccountModel>(loginViewModel);
        try
        {
            var token = await _accountService.Login(loginModel);
            HttpContext.Response.Cookies.Append("token", token,
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddHours(3),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                });
        }
        catch (Exception)
        {
            TempData["Error"] = "Wrong credentials. Please try again.";
            return View(loginViewModel);
        }
        
        
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) 
            return View(registerViewModel);

        var registerModel = _mapper.Map<RegisterUserAccountModel>(registerViewModel);
        try
        {
            await _accountService.Register(registerModel);
        }
        catch (Exception e)
        {
            TempData["Error"] = e.Message;
            return View(registerViewModel);
        }

        return RedirectToAction(nameof(Login));
    }
}
