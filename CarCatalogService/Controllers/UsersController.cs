using AutoMapper;
using CarCatalogService.Services.UserService;
using CarCatalogService.Services.UserService.Models;
using CarCatalogService.Shared;
using CarCatalogService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers;

[Authorize(Policy = AppRoles.Admin)]
public class UsersController : Controller
{
    private readonly IUserSevice _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserSevice userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {            
        var response = await _userService.GetAllUsers();
        return View(response);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddUserViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userViewModel);
        }

        var userModel = _mapper.Map<AddUserModel>(userViewModel);

        await _userService.AddUser(userModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var user = await _userService.GetUser(id);
        if (user == null)
            return View("Error");

        var userViewModel = _mapper.Map<EditUserViewModel>(user);
        return View(userViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(long id, EditUserViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit car");
            return View(nameof(Edit), userViewModel);
        }

        var userModel = _mapper.Map<UpdateUserModel>(userViewModel);
        try
        {
            await _userService.UpdateUser(id, userModel);
        }
        catch (Exception e)
        {
            TempData["Error"] = e.Message;
            return View(userViewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        var userModel = await _userService.GetUser(id);
        if (userModel == null)
            return View("Error");

        return View(userModel);
    }

    [HttpPost, ActionName(nameof(Delete))]
    public async Task<IActionResult> DeleteUser(long id)
    {
        var userModel = await _userService.GetUser(id);
        if (userModel == null)
            return View("Error");

        await _userService.DeleteUser(id);

        return RedirectToAction(nameof(Index));
    }
}
