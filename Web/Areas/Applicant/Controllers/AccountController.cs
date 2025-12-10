using DbEntities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helper;
using Web.Model;
using Web.Model.Identity;
using Web.Service.Identity.Interface;
using Web.Service.Interface;

namespace Web.Areas.Applicant.Controllers
{
    [Area("Applicant")]
    [Route("Applicant/[controller]")]
    [Authorize]
    public class AccountController(UserManager<ApplicationUser> userManager,
                                   SignInManager<ApplicationUser> signInManager,
                                   IApplicantService applicantService,
                                   IUserRoleService userRoleService,
                                   IRoleManagerService roleManagerService,
                                   IApplicationUserManagerService applicationUserManagerService) : BaseController
    {

        [HttpGet]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnURL = null)
        {
            if (signInManager.IsSignedIn(User))
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var roles = await userManager.GetRolesAsync(user);

                if (roles.Contains("Manager") || roles.Contains("Admin"))
                {
                    TempData["SuccessMessage"] = "شما با موفقیت وارد شدید.";
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else if (roles.Contains("Applicant"))
                {
                    TempData["SuccessMessage"] = "شما با موفقیت وارد شدید.";
                    return RedirectToAction("Index", "Dashboard", new { area = "Applicant" });
                }
            }

            ViewData["ReturnURL"] = returnURL;
            return View(new SignInViewModel());
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(SignInViewModel model, string returnURL)
        {
            ViewData["ReturnURL"] = returnURL;

            if (!ModelState.IsValid)
            {

                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();
                return View(model);
            }

            var applicationUserViewModel = applicationUserManagerService.GetByPhoneNumber(model.Mobile);
            if (applicationUserViewModel == null)
            {
                TempData["ErrorMessage"] = "کاربری با این شماره موبایل یافت نشد.";
                return View(model);
            }

            var user = await userManager.FindByIdAsync(applicationUserViewModel.Id.ToString());
            if (user == null)
            {
                TempData["ErrorMessage"] = "کاربر در سیستم یافت نشد.";
                return View(model);
            }

            var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (passwordCheck.Succeeded)
            {

                await signInManager.SignInAsync(user, isPersistent: model.RememberMe);

                var roles = await userManager.GetRolesAsync(user);

                if (roles.Contains("Manager") || roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else if (roles.Contains("Applicant"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Applicant" });
                }
                return RedirectToLocal(returnURL);
            }
            else
            {
                if (passwordCheck.IsLockedOut)
                {
                    TempData["ErrorMessage"] = "اکانت شما قفل شده است.";
                    return View(model);
                }
                if (passwordCheck.IsNotAllowed)
                {
                    TempData["ErrorMessage"] = "ورود برای این کاربر مجاز نیست.";
                    return View(model);
                }
                if (passwordCheck.RequiresTwoFactor)
                {
                    // redirect to 2FA flow
                    return RedirectToAction(nameof(LoginWith2fa), new { returnURL, model.RememberMe });
                }

                TempData["ErrorMessage"] = "رمز عبور یا نام کاربری اشتباه است";
                return View(model);
            }
        }

        // optional helper
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Route("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(string returnURL = null)
        {
            if (signInManager.IsSignedIn(User))
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var roles = await userManager.GetRolesAsync(user);

                if (roles.Contains("Manager") || roles.Contains("Admin"))
                {
                    TempData["SuccessMessage"] = "شما با موفقیت وارد شدید.";
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else if (roles.Contains("Applicant"))
                {
                    TempData["SuccessMessage"] = "شما با موفقیت وارد شدید.";
                    return RedirectToAction("Index", "Dashboard", new { area = "Applicant" });
                }
            }

            ViewData["ReturnURL"] = returnURL;
            return View(new SignUpViewModel());
        }

        [HttpPost]
        [Route("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpViewModel model, string returnURL)
        {
            ViewData["ReturnURL"] = returnURL;


            if (ModelState.IsValid)
            {
                var applicationUserViewModel = applicationUserManagerService.GetByPhoneNumber(model.Mobile);

                if (applicationUserViewModel != null)
                {
                    TempData["SuccessMessage"] = "حساب شما قبلا در سامانه ثبت شده است به صفحه ورود منتقل میشوید.";
                    return RedirectToAction("Login", "Account", new { area = "Applicant" });
                }

                var applicantViewModel = new ApplicantViewModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Mobile = model.Mobile,
                    Gender = model.Gender,
                    StudyFieldId = null,
                    CityId = null,
                    ProvinceId = null

                };

                var applicantId = applicantService.Add(applicantViewModel, model.Password);

                if (applicantId == -1)
                {
                    TempData["ErrorMessage"] = "خطا در ثبت نام";
                    return RedirectToAction("SignUp", "Account", new { Area = "Applicant" });
                }

                var applicationUser = new ApplicationUser()
                {
                    ApplicantId = applicantId,
                    Name = model.FirstName + " " + model.LastName,
                    UserName = Guid.NewGuid().ToString(),
                    PhoneNumber = model.Mobile
                };


                var isCreated = await userManager.CreateAsync(applicationUser, model.Password);
                if (isCreated.Succeeded)
                {
                    var user = applicationUserManagerService.GetUserByApplicantId(applicantId);
                    var role = roleManagerService.GetAll().FirstOrDefault(x => x.Name == "Applicant");
                    var userRoleViewModel = new UserRoleViewModel() { UserId = user.Id, RoleId = role.Id };
                    var isAdd = userRoleService.Add(userRoleViewModel);

                    if (isAdd)
                    {
                        TempData["SuccessMessage"] = "حساب شما با موفقیت ساخته شد.";
                        return RedirectToAction("Login", "Account", new { area = "Applicant" });
                    }
                }

                TempData["ErrorMessage"] = "خطا در ثبت نام";
                return RedirectToAction("Login", "Account", new { area = "Applicant" });

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

        [HttpGet]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string mobile)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var applicationUserViewModel = applicationUserManagerService.GetByPhoneNumber(mobile);
            var user = await userManager.FindByIdAsync(applicationUserViewModel.Id.ToString());
            var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var newPassword = PasswordGenerator.GenerateIdentityPassword();
            var result = await userManager.ResetPasswordAsync(user, resetPasswordToken, newPassword);

            if (result.Succeeded)
            {
                if (applicationUserManagerService.SendPassword(user.Id, newPassword))
                {
                    TempData["SuccessMessage"] = "رمزعبور جدید برای شما ارسال شد لطفا پس از ورود آن را در ویرایش نمایید.";
                    return RedirectToAction("Login", "Account", new { Area = "Applicant" });
                }

            }
            else
            {
                TempData["ErrorMessage"] = "خطا لطفا مجدد تلاش کنید";
                return RedirectToAction("ForgotPassword", "Account", new { Area = "Applicant" });
            }

            TempData["ErrorMessage"] = "خطا لطفا مجدد تلاش کنید";
            return RedirectToAction("ForgotPassword", "Account", new { Area = "Applicant" });
        }

        [HttpGet]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var applicationUserViewModel = applicationUserManagerService.GetCurrentUser();
            var user = await userManager.FindByIdAsync(applicationUserViewModel.Id.ToString());
            var isCurrentPasswordCorrect = await userManager.CheckPasswordAsync(user, model.CurrentPassword);

            if (!isCurrentPasswordCorrect)
            {
                TempData["ErrorMessage"] = "رمزعبور فعلی اشتباه است";
                return View(model);
            }

            var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, resetPasswordToken, model.NewPassword);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "رمزعبور با موفقیت تغییر یافت.";
                return RedirectToAction("Login", "Account", new { Area = "Applicant" });
            }

            TempData["ErrorMessage"] = "خطا در تغییر رمزعبور";
            return RedirectToAction("Index", "Dashboard", new { Area = "Applicant" });
        }

        [HttpGet]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            TempData["SuccessMessage"] = "خارج شدید.";
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

    }

    public class SignInViewModel
    {
        [Display(Name = "نام کاربری")]
        public string Mobile { get; set; }

        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    public class SignUpViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public bool Gender { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "رمزعبور و تائید رمزعبور باید یکسان باشند.")]
        public string ConfirmNewPassword { get; set; }
    }
}
