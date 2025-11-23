using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class Skill_ExamResource_Controller(ISkill_ExamResource_Service skill_ExamResource_Service, ISkillService skillService) : BaseController
    {
        [Route("Index/{skillId}")]
        public IActionResult Index(int skillId)
        {
            var skillViewModel = skillService.Get(skillId);

            if (skillViewModel == null)
            {
                ShowDangerToast("", "مهارت یافت نشد.");
            }

            ViewBag.SkillId = skillViewModel.SkillId;
            ViewBag.SkillName = skillViewModel.SkillName;
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request, string filterSkillId,
                                            string filterExamResourceId, string filterInsertDateFrom, string filterInsertDateTo)
        {

            var result = new DataSourceResult()
            {
                Data = skill_ExamResource_Service.GetAllFiltered(filterSkillId, filterExamResourceId, 
                                                                 filterInsertDateFrom, filterInsertDateTo, request.Page,
                                                                 request.PageSize, out int totalRecord),
                Total = totalRecord
            };

            return Json(result);
        }

        [HttpGet]
        [Route("Create/{skillId}")]
        public IActionResult Create(int skillId)
        {
            var skillViewModel = skillService.Get(skillId);

            if (skillViewModel == null)
            {
                ShowDangerToast("", "مهارت یافت نشد.");
            }

            ViewBag.SkillId = skillViewModel.SkillId;
            ViewBag.SkillName = skillViewModel.SkillName;
            return View();
        }

        [HttpPost]
        [Route("Create/{skillId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Skill_ExamResource_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!skill_ExamResource_Service.IsDuplicate(model.ExamResourceId, model.SkillId))
                {
                    if (skill_ExamResource_Service.Add(model.ExamResourceId, model.SkillId))
                    {
                        ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                        return RedirectToAction("Index", new { skillId = model.SkillId });
                    }
                    else
                    {
                        ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                        return RedirectToAction("Index", new { skillId = model.SkillId });
                    }
                }
                else
                {
                    ShowDangerToast(null, "این منبع آزمون برای این مهارت وجود دارد");
                    return RedirectToAction("Index", new { skillId = model.SkillId });
                }
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                ShowDangerToast(null, "اطلاعات واردشده معتبر نیست");
                return RedirectToAction("Index", new { skillId = model.SkillId });
            }
            return View(model);
        }

        [HttpGet]
        [Route("Edit/{skillId}/{examResourceId}")]
        public IActionResult Edit(int skillId, int examResourceId)
        {
            var model = skill_ExamResource_Service.Get(examResourceId, skillId);
            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Skill_ExamResource_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (skill_ExamResource_Service.Edit(model))
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index", new { skillId = model.SkillId });
                }
                else
                {
                    ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
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
        public bool Delete(int skillId, int examResourceId)
        {
            return skill_ExamResource_Service.Delete(examResourceId, skillId);
        }

        [Route("Fill_Skill_ExamResource_Combo")]
        public virtual JsonResult Fill_Skill_ExamResource_Combo(int skillId)
        {
            var DataList = skill_ExamResource_Service.GetAllBySkillId(skillId)
                                                     .Select(x =>
                                                                 new SelectListItem
                                                                 {
                                                                     Text = x.SkillName,
                                                                     Value = x.SkillId.ToString()
                                                                 });
            return Json(DataList);
        }
    }
}
