﻿using AutoMapper;
using CarCatalogService.DAL.Entities;
using CarCatalogService.BLL.Services.AccountService;
using CarCatalogService.BLL.Services.AccountService.Models;
using CarCatalogService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AccountController(IAccountService accountService, IMapper mapper, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _accountService = accountService;
        _mapper = mapper;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
            return View(loginViewModel);

        var loginModel = _mapper.Map<LoginUserAccountModel>(loginViewModel);
        try
        {
            var token = await _accountService.LoginAsync(loginModel);
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
            await _accountService.RegisterAsync(registerModel);
        }
        catch (Exception e)
        {
            TempData["Error"] = e.Message;
            return View(registerViewModel);
        }

        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("token");
        return RedirectToAction("Login", "Account");
    }
}
