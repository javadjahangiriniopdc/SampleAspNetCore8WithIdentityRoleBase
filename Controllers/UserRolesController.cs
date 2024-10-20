using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleAspNetCore8WithIdentityRoleBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SampleAspNetCore8WithIdentityRoleBase.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var viewModel = new UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user)
                };
                userRolesViewModel.Add(viewModel);
            }

            return View(userRolesViewModel);
        }

        public async Task<IActionResult> Manage(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _roleManager.Roles.ToListAsync();
            var selectedRoles = await _userManager.GetRolesAsync(user);

            var model = new ManageUserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new UserRoleModel
                {
                    RoleName = role.Name,
                    IsSelected = selectedRoles.Contains(role.Name) // وضعیت انتخاب را تنظیم کنید
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(ManageUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            // دریافت نقش‌های فعلی کاربر
            var roles = await _userManager.GetRolesAsync(user);
            // حذف تمامی نقش‌های فعلی
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            // اضافه کردن نقش‌های انتخاب‌شده
            var selectedRoleNames = model.Roles
                .Where(r => r.IsSelected)
                .Select(r => r.RoleName)
                .ToList();

            // اضافه کردن نقش‌های جدید
            result = await _userManager.AddToRolesAsync(user, selectedRoleNames);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}
