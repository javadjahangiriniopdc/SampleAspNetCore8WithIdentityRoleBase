using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SampleAspNetCore8WithIdentityRoleBase.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
    }
}
