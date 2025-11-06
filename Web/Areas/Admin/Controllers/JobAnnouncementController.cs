using System;
using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class JobAnnouncementController(
        IJobAnnouncementService jobAnnouncementService,
        IJobAnnouncement_Skill_Service ja_skill_serivce,
        IJobAnnouncement_StudyField_Service ja_studyField_service,
        IJobAnnouncement_JobAnnouncementCategory_Service ja_jac_service)
        : BaseController
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            string filterTitle, string filterGender, string filterHasEmploymentExam, string filterPublishDateFrom,
            string filterPublishDateTo, string filterExamDateFrom, string filterExamDateTo,
            string filterCapacityFrom, string filterCapacityTo, string filterActive)
        {
            //Paging and Sorting
            int currentPage = request.Page;
            int pageSize = request.PageSize;

            int totalRecord = 0;


            var result = new DataSourceResult()
            {
                Data = jobAnnouncementService.GetAllFiltered(filterTitle, filterGender, filterHasEmploymentExam,
                filterPublishDateFrom, filterPublishDateTo, filterExamDateFrom, filterExamDateTo,
                filterCapacityFrom, filterCapacityTo, filterActive,null, null, currentPage, pageSize, out totalRecord),
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
        public virtual ActionResult Create(JobAnnouncementViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<int> jaCategoryIdList = new List<int>();
                foreach (string jaCategoryId in Request.Form["CategoryMultiSelect"].ToList())
                {
                    jaCategoryIdList.Add(Convert.ToInt32(jaCategoryId));
                }

                List<int> skillIdList = new List<int>();
                foreach (string skillId in Request.Form["SkillMultiSelect"].ToList())
                {
                    skillIdList.Add(Convert.ToInt32(skillId));
                }

                List<int> studyFieldIdList = new List<int>();
                foreach (string studyFieldId in Request.Form["StudyFieldMultiSelect"].ToList())
                {
                    studyFieldIdList.Add(Convert.ToInt32(studyFieldId));
                }

                int jobAnnouncementId = jobAnnouncementService.Add(model);
                if (jobAnnouncementId != -1)
                {
                    if (jaCategoryIdList.Any())
                    {
                        foreach (var jaCategoryId in jaCategoryIdList)
                        {
                            ja_jac_service.Add(jobAnnouncementId, jaCategoryId);
                        }
                    }

                    if (skillIdList.Any())
                    {
                        foreach (var skillId in skillIdList)
                        {
                            ja_skill_serivce.Add(jobAnnouncementId, skillId);
                        }
                    }

                    if (studyFieldIdList.Any())
                    {
                        foreach (var studyFiledId in studyFieldIdList)
                        {
                            ja_studyField_service.Add(jobAnnouncementId, studyFiledId);
                        }
                    }

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
            var model = jobAnnouncementService.Get(id);

            List<string> jaCategoryIdList = new List<string>();
            List<string> skillIdList = new List<string>();
            List<string> studyFiledIdList = new List<string>();

            foreach (var ja_jac in ja_jac_service.GetAllByJobAnnouncementId(id))
            {
                jaCategoryIdList.Add(ja_jac.JobAnnouncementCategoryId.ToString());
            }

            foreach (var ja_skill in ja_skill_serivce.GetAllByJobAnnouncementId(id))
            {
                skillIdList.Add(ja_skill.SkillId.ToString());
            }

            foreach (var ja_studyField in ja_studyField_service.GetAllByJobAnnouncementId(id))
            {
                studyFiledIdList.Add(ja_studyField.StudyFieldId.ToString());
            }

            if (jaCategoryIdList.Any())
            {
                ViewBag.JACategoryIdArray = jaCategoryIdList.ToArray();
            }
            else
            {
                ViewBag.JACategoryIdArray = null;
            }

            if (skillIdList.Any())
            {
                ViewBag.SkillIdArray = skillIdList.ToArray();
            }
            else
            {
                ViewBag.SkillIdArray = null;
            }

            if (studyFiledIdList.Any())
            {
                ViewBag.SkillIdArray = skillIdList.ToArray();
            }
            else
            {
                ViewBag.SkillIdArray = null;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(JobAnnouncementViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<int> jaCategoryIdList = new List<int>();
                foreach (string jaCategoryId in Request.Form["JA_Category_MultiSelect"].ToList())
                {
                    jaCategoryIdList.Add(Convert.ToInt32(jaCategoryId));
                }

                List<int> skillIdList = new List<int>();
                foreach (string skillId in Request.Form["SkillMultiSelect"].ToList())
                {
                    skillIdList.Add(Convert.ToInt32(skillId));
                }

                List<int> studyFieldIdList = new List<int>();
                foreach (string studyFieldId in Request.Form["StudyFieldMultiSelect"].ToList())
                {
                    studyFieldIdList.Add(Convert.ToInt32(studyFieldId));
                }

                if (jobAnnouncementService.Edit(model))
                {
                    foreach(var ja_jac in ja_jac_service.GetAllByJobAnnouncementId(model.JobAnnouncementId))
                    {
                        ja_jac_service.Delete(model.JobAnnouncementId, ja_jac.JobAnnouncementCategoryId);
                    }

                    foreach (var ja_skill in ja_skill_serivce.GetAllByJobAnnouncementId(model.JobAnnouncementId))
                    {
                        ja_skill_serivce.Delete(model.JobAnnouncementId, ja_skill.SkillId);
                    }

                    foreach (var ja_studyField in ja_studyField_service.GetAllByJobAnnouncementId(model.JobAnnouncementId))
                    {
                        ja_studyField_service.Delete(model.JobAnnouncementId, ja_studyField.StudyFieldId);
                    }

                    if (jaCategoryIdList.Any())
                    {
                        foreach (var jaCategoryId in jaCategoryIdList)
                        {
                            ja_jac_service.Add(model.JobAnnouncementId, jaCategoryId);
                        }
                    }

                    if (skillIdList.Any())
                    {
                        foreach (var skillId in skillIdList)
                        {
                            ja_skill_serivce.Add(model.JobAnnouncementId, skillId);
                        }
                    }

                    if (studyFieldIdList.Any())
                    {
                        foreach (var studyFiledId in studyFieldIdList)
                        {
                            ja_studyField_service.Add(model.JobAnnouncementId, studyFiledId);
                        }
                    }

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
            return jobAnnouncementService.Delete(id);
        }
    }
}
