using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // ایجاد نقش‌ها
        string[] roleNames = { "Admin", "Manager", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // ایجاد نقش جدید
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // ایجاد کاربر admin
        var adminEmail = "admin@admin.com";
        var adminPassword = "Admin@1234567";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new IdentityUser()
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, adminPassword);
        }

        // اضافه کردن کاربر به نقش Admin
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}