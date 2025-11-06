using System;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Model.Identity;
using DbEntities.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Web.Service.Identity.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class UserController(
        IRoleManagerService roleManager,
        IUserRoleService userRoleService,
        UserManager<ApplicationUser> userManager,
        IApplicationUserManagerService userManagerService)
        : BaseController
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public virtual ActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            string filterUserName, string filterName, string filterRoleId)
        {
            //Paging and Sorting
            int currentPage = request.Page;
            int pageSize = request.PageSize;
            string sortDirection = "ASC";
            string sortField = "Id";

            if (request.Sorts != null && request.Sorts.Count > 0)
            {
                sortField = request.Sorts[0].Member;
                sortDirection = request.Sorts[0].SortDirection.ToString();
            }
            int totalRecord = 0;

            var result = new DataSourceResult()
            {
                Data = userManagerService.GetAllFiltered(filterUserName, filterName, filterRoleId,
                currentPage: request.Page, pageSize: request.PageSize, sortField: sortField, sortDirection: sortDirection,
                currentUserIsManager: User.IsInRole("Manager"),
                totalRecord: out totalRecord),
                Total = totalRecord // Total number of records
            };

            return Json(result);
        }

        [HttpGet("Create")]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(CreateUserViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = userViewModel.UserName,
                        Email = userViewModel.Email,
                        PhoneNumber = userViewModel.PhoneNumber,
                        Name = userViewModel.Name,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };
                    var createResult = await userManager.CreateAsync(user, userViewModel.Password).ConfigureAwait(false);

                    if (createResult.Succeeded)
                    {
                        //Add User to the selected Roles
                        string roleName = roleManager.Get(userViewModel.RoleId).Name;
                        var addToRoleResult = await userManager.AddToRoleAsync(user, roleName).ConfigureAwait(false);
                        if (!addToRoleResult.Succeeded)
                        {
                            ShowDangerToast(null, addToRoleResult.Errors.First().Description);
                            return View();
                        }
                    }
                    else
                    {
                        ShowDangerToast(null, createResult.Errors.First().Description);
                        return View();

                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ShowDangerToast(null, "خطایی رخ داد: " + e.Message);
            }
            return View(userViewModel);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public virtual async Task<ActionResult> Edit(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString()).ConfigureAwait(false);

            var userRoles = userRoleService.GetAllByUserId(id);

            return View(new EditUserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                RoleId = userRoles[0].RoleId
            });
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id.ToString()).ConfigureAwait(false);

                user.Name = model.Name;
                user.UserName = model.Username;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                if (model.Password != null)
                {
                    user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
                }

                var userRoles = await userManager.GetRolesAsync(user).ConfigureAwait(false);

                var result = await userManager.RemoveFromRolesAsync(user, userRoles.ToArray()).ConfigureAwait(false);

                if (!result.Succeeded)
                {
                    ShowDangerToast(null, result.Errors.First().Description);
                }
                await userManager.UpdateSecurityStampAsync(user).ConfigureAwait(false);

                if (User.IsInRole("Manager"))
                {
                    string[] myRoles = new string[] { roleManager.Get(model.RoleId).Name };
                    result = await userManager.AddToRolesAsync(user, myRoles).ConfigureAwait(false);

                    if (!result.Succeeded)
                    {
                        ShowDangerToast(null, result.Errors.First().Description);
                    }
                }

                await userManager.UpdateSecurityStampAsync(user).ConfigureAwait(false);

                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            else
            {
                var errors = ModelState
               .Where(x => x.Value.Errors.Count > 0)
               .Select(x => new { x.Key, x.Value.Errors })
               .ToArray();

                ShowDangerToast(null, "اطلاعات واردشده نامعتبر است");
            }

            return View(model);
        }

        [Route("Delete")]
        public short Delete(int userId)
        {
            if (userId == Convert.ToInt32(userManagerService.GetCurrentUserId()))
            {
                return -1;
            }

            var user = userManagerService.Get(userId);

            if (user == null) { return -2; }

            if (user.RoleName == "داوطلب")
            {
                return -3;
            }

            if (userManagerService.DeleteUser(userId))
            {
                return 1;
            }
            else
            {
                return -4;
            }
        }

        [Route("Fill_Role_Combo")]
        public virtual JsonResult Fill_Role_Combo(bool includeApplicantRole)
        {
            List<SelectListItem> itemsList = new List<SelectListItem>();


            if (User.IsInRole("Manager"))
            {
                SelectListItem adminRole = new SelectListItem()
                {
                    Text = "ادمین",
                    Value = "1"
                };

                itemsList.Add(adminRole);
            }

            if (includeApplicantRole)
            {
                SelectListItem applicantRole = new SelectListItem()
                {
                    Text = "داوطلب",
                    Value = "3"
                };

                itemsList.Add(applicantRole);
            }

            var DataList = itemsList.Select(x =>
                new SelectListItem
                {
                    Text = x.Text,
                    Value = x.Value
                });

            return Json(DataList);
        }
    }
}
