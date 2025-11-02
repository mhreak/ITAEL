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
    public class StudyFieldController : BaseController
    {
        readonly IStudyFieldService _studyFieldService;

        public StudyFieldController(IStudyFieldService studyFieldService)
        {
            _studyFieldService = studyFieldService;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            string filterStudyFieldName, string filterActive)
        {
            //Paging and Sorting
            int currentPage = request.Page;
            int pageSize = request.PageSize;

            int totalRecord = 0;

            
            var result = new DataSourceResult()
            {
                Data = _studyFieldService.GetAllFiltered(filterStudyFieldName, filterActive, currentPage, pageSize, out totalRecord),
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
        public virtual ActionResult Create(StudyFieldViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_studyFieldService.IsDuplicateByName(null, model.StudyFieldName))
                {
                    if (_studyFieldService.Add(model) != -1)
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
                    ShowDangerToast(null, "یک رشته تحصیلی دیگر با این نام وجود دارد");
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
            var model = _studyFieldService.Get(id);
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(StudyFieldViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_studyFieldService.IsDuplicateByName(model.StudyFieldId, model.StudyFieldName))
                {
                    if (_studyFieldService.Edit(model))
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
                    ShowDangerToast(null, "یک رشته تحصیلی دیگر با این نام وجود دارد");
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
            return _studyFieldService.Delete(id);
        }

        [Route("Fill_StudyField_Combo")]
        public virtual JsonResult Fill_StudyField_Combo(bool? active)
        {
            var DataList = _studyFieldService.GetAll(active)
                .Select(x =>
                new SelectListItem
                {
                    Text = x.StudyFieldName,
                    Value = x.StudyFieldId.ToString()
                });
            return Json(DataList);
        }
    }
}
