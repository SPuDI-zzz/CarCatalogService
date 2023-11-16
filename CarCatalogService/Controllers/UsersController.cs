using CarCatalogService.Services.RoleService;
using CarCatalogService.Services.UserService;
using CarCatalogService.Services.UserService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarCatalogService.Controllers
{
    public class UsersController : Controller
    {
        private protected IUserSevice _userService;
        private readonly IRoleService _roleService;

        public UsersController(IUserSevice userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {            
            var response = await _userService.GetAllUsers();
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            var roles = await _roleService.GetAllRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddUserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                var roles = await _roleService.GetAllRoles();
                ViewBag.Roles = new SelectList(roles, "Id", "Name");
                return View(userModel);
            }
            await _userService.AddUser(userModel);
            return RedirectToAction(nameof(Index));
        }
    }
}
