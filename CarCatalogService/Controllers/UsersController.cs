using AutoMapper;
using CarCatalogService.BLL.Services.UserService;
using CarCatalogService.BLL.Services.UserService.Models;
using CarCatalogService.Shared.Const;
using CarCatalogService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers;

/// <summary>
///     Controller responsible for managing user-related actions.
/// </summary>
[Authorize(Policy = AppRoles.Admin)]
public class UsersController : Controller
{
    private readonly IUserSevice _userService;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UsersController"/> class.
    /// </summary>
    /// <param name="userService">The user service for managing user-related operations.</param>
    /// <param name="mapper">The AutoMapper instance for mapping between different object types.</param>
    public UsersController(IUserSevice userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    /// <summary>
    ///     Displays a list of all users.
    /// </summary>
    /// <returns>The view containing the list of all users.</returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> Index()
    {            
        var response = await _userService.GetAllUsersAsync();
        return View(response);
    }

    /// <summary>
    ///     Displays the view for creating a new user.
    /// </summary>
    /// <returns>The view for creating a new user.</returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    ///     Handles the POST request for creating a new user.
    /// </summary>
    /// <param name="userViewModel">The view model containing user information for creation.</param>
    /// <returns>
    ///     Redirects to the user list view on successful creation; otherwise,
    ///     returns the create view with an error message.
    /// </returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> Create(AddUserViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userViewModel);
        }

        var userModel = _mapper.Map<AddUserModel>(userViewModel);
        try
        {
            await _userService.AddUserAsync(userModel);
        }
        catch (Exception e)
        {
            TempData["Error"] = e.Message;
            return View(userViewModel);
        }
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    ///     Displays the view for editing a user.
    /// </summary>
    /// <param name="id">The unique identifier of the user to edit</param>
    /// <returns>
    ///     If the user is found, returns the edit confirmation view with details of the user;
    ///     otherwise, returns an error view.
    /// </returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var user = await _userService.GetUserAsync(id);
        if (user == null)
            return View("Error");

        var userViewModel = _mapper.Map<EditUserViewModel>(user);
        return View(userViewModel);
    }

    /// <summary>
    ///     Handles the HTTP POST request for editing an existing user.
    /// </summary>
    /// <param name="id">The unique identifier of the user to edit.</param>
    /// <param name="userViewModel">The view model containing updated information for the user.</param>
    /// <returns>
    ///     Redirects to the user list view on successful update; otherwise, returns the edit view with an error message.
    /// </returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
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
            await _userService.UpdateUserAsync(id, userModel);
        }
        catch (Exception e)
        {
            TempData["Error"] = e.Message;
            return View(userViewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    ///     Displays the view for deleting a user.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <returns>
    ///     If the user is found, returns the delete confirmation view with details of the user;
    ///     otherwise, returns an error view.
    /// </returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        var userModel = await _userService.GetUserAsync(id);
        if (userModel == null)
            return View("Error");

        return View(userModel);
    }

    /// <summary>
    ///     Handles the HTTP POST request for deleting an existing user.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <returns>
    ///     Redirects to the user list view on successful delete; otherwise, returns an error view.
    /// </returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
    [HttpPost, ActionName(nameof(Delete))]
    public async Task<IActionResult> DeleteUser(long id)
    {
        var userModel = await _userService.GetUserAsync(id);
        if (userModel == null)
            return View("Error");

        await _userService.DeleteUserAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
