using CarCatalogService.Services.RoleService;
using CarCatalogService.Services.RoleService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers
{
    public class RolesController : Controller
    {
        private protected IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _roleService.GetAllRoles();
            return View(response);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddRoleModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            await _roleService.AddRole(userModel);
            return RedirectToAction(nameof(Index));
        }
    }
}
