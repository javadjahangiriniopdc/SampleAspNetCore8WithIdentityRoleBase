using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SampleAspNetCore8WithIdentityRoleBase.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                ModelState.AddModelError("", "Role name cannot be empty.");
                return View();
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                ModelState.AddModelError("", "Role already exists.");
                return View();
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        // Edit role (GET)
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // Edit role (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
            {
                ModelState.AddModelError("", "Role name cannot be empty.");
                return View(role);
            }

            var existingRole = await _roleManager.FindByIdAsync(role.Id);
            if (existingRole == null)
            {
                return NotFound();
            }

            existingRole.Name = role.Name;

            var result = await _roleManager.UpdateAsync(existingRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(role);
        }

        // Delete role (GET)
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // Delete role (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(role);
        }
    }
}
