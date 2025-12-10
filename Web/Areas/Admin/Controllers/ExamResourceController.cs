using DbEntities;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Linq;
using Web.Controllers;
using Web.Model;
using Web.Service;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class ExamResourceController(
        IUtilService utilService,
        IWebHostEnvironment webHostEnvironment,
        IExamResourceService examResourceService) : BaseController
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request, string filterResourceName,
                                            string filterType, string filterPriceFrom, string filterPriceTo,
                                            string filterInsertDateFrom, string filterInsertDateTo)
        {
            //Paging and Sorting

            int currentPage = request.Page;
            int pageSize = request.PageSize;

            var result = new DataSourceResult()
            {
                Data = examResourceService.GetAllFiltered(filterResourceName, null,
                                                           filterType, filterPriceFrom, filterPriceTo,
                                                           filterInsertDateFrom, filterInsertDateTo,
                                                           currentPage, pageSize, out int totalRecord),
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
        public IActionResult Create(ExamResourceViewModel model, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                int examResourceId = examResourceService.Add(model);
                if (examResourceId != -1)
                {
                    string directoryPath = webHostEnvironment.WebRootPath + "\\Upload\\ExamResourceDocument\\" + examResourceId.ToString();

                    if (Directory.Exists(directoryPath))
                    {
                        Directory.Delete(directoryPath, true);
                    }

                    Directory.CreateDirectory(directoryPath);

                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        string uploadedFileName = ImageFile.FileName;
                        string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                        string uploadedFileExtention = ImageFile.FileName.Split('.')[splittedFileName.Length - 1];
                        string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                        string filePath = directoryPath + "\\" + generatedFileName;

                        while (System.IO.File.Exists(filePath))
                        {
                            generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                            filePath = directoryPath + "\\" + generatedFileName;
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageFile.CopyTo(stream);

                            examResourceService.SetImageFileName(examResourceId, generatedFileName);

                            stream.Close();
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
            var model = examResourceService.Get(id);
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ExamResourceViewModel model, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (examResourceService.Edit(model))
                {
                    string directoryPath = webHostEnvironment.WebRootPath + "\\Upload\\ExamResourceDocument\\" + model.ExamResourceId.ToString();

                    if (Directory.Exists(directoryPath))
                    {
                        Directory.Delete(directoryPath, true);
                    }

                    Directory.CreateDirectory(directoryPath);

                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        string uploadedFileName = ImageFile.FileName;
                        string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                        string uploadedFileExtention = ImageFile.FileName.Split('.')[splittedFileName.Length - 1];
                        string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                        string filePath = directoryPath + "\\" + generatedFileName;

                        while (System.IO.File.Exists(filePath))
                        {
                            generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                            filePath = directoryPath + "\\" + generatedFileName;
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageFile.CopyTo(stream);

                            examResourceService.SetImageFileName(model.ExamResourceId, generatedFileName);

                            stream.Close();
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
            return examResourceService.Delete(id);
        }

        [Route("Fill_ExamResource_Combo")]
        public virtual JsonResult Fill_ExamResource_Combo()
        {
            var DataList = examResourceService.GetAll()
                                                   .Select(x =>
                                                               new SelectListItem
                                                               {
                                                                   Text = x.ResourceName,
                                                                   Value = x.ExamResourceId.ToString()
                                                               });
            return Json(DataList);
        }

        [Route("ShowExamResourceSearchDialog")]
        public virtual ActionResult ShowExamResourceSearchDialog(string valueElementId, string displayElementId)
        {
            ViewBag.ValueElementId = valueElementId;
            ViewBag.DisplayElementId = displayElementId;

            return PartialView("_ExamResourceSearchDialog");
        }
    }
}
