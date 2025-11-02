using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Web.Controllers;
using Web.Model;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class WalletController : BaseController
    {
        readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            string filterWalletName, string filterActive,
            string filterInsertDateFrom, string filterInsertDateTo)
        {
            //Paging and Sorting
            int currentPage = request.Page;
            int pageSize = request.PageSize;

            int totalRecord = 0;


            var result = new DataSourceResult()
            {
                Data = _walletService.GetAllFiltered(filterWalletName,
                filterActive, filterInsertDateFrom, filterInsertDateTo, currentPage, pageSize, out totalRecord),
                Total = totalRecord // Total number of records
            };

            return Json(result);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(WalletViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_walletService.IsDuplicateByWalletName(null, model.WalletName))
                {
                    if (_walletService.Add(model) != -1)
                    {
                        ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                    }
                }
                else
                {
                    ShowDangerToast(null, "یک کیف پول دیگر با این نام وجود دارد");
                }
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                ShowDangerToast(null, "اطلاعات واردشده معتبر نیست");
            }
            return View(model);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var model = _walletService.Get(id);
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(WalletViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_walletService.IsDuplicateByWalletName(model.WalletId, model.WalletName))
                {
                    if (_walletService.Edit(model))
                    {
                        ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                    }
                }
                else
                {
                    ShowDangerToast(null, "یک کیف پول دیگر با این نام وجود دارد");
                }
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                ShowDangerToast(null, "اطلاعات واردشده معتبر نیست");
            }
            return View(model);
        }

        [Route("Delete")]
        public bool Delete(int id)
        {
            return _walletService.Delete(id);
        }

        [Route("Fill_Wallet_Combo")]
        public virtual JsonResult Fill_Wallet_Combo()
        {
            var DataList = _walletService.GetAll()
                .Select(x =>
                new SelectListItem
                {
                    Text = x.WalletName,
                    Value = x.WalletId.ToString()
                });
            return Json(DataList);
        }
    }
}
