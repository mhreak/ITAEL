using DbEntities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Web.Model.Identity;

namespace Web.Controllers
{
    public class AccountController : BaseController
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Login(string returnURL = null)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Manager") || roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
                else if(roles.Contains("Applicant"))
                {
                    return RedirectToAction("Index", "Panel", new { area = "Applicant" });
                }
            }

            ViewData["ReturnURL"] = returnURL;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnURL)
        {
            ViewData["ReturnURL"] = returnURL;


            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                var result =
                await
                    _signInManager.PasswordSignInAsync(model.UserName, model.Password,
                    model.RememberMe, false).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Manager") || roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Applicant", new { area = "Admin" });
                    }
                    else if(roles.Contains("Applicant"))
                    {
                        return RedirectToAction("Index", "Panel", new { area = "Applicant" });
                    }
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        ViewData["ErrorMessage"] = "اکانت شما قفل شده است";
                    }
                }

                ModelState.AddModelError("", "رمز عبور یا نام کاربری اشتباه است");

            }
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
