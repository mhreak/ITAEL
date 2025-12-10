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
    public class CompanyController(ICompanyService companyService, IWebHostEnvironment webHostEnvironment, IUtilService utilService) : BaseController
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            string filterCompanyName, string filterInsertDateFrom, string filterInsertDateTo)
        {
            //Paging and Sorting
            
            int currentPage = request.Page;
            int pageSize = request.PageSize;

            int totalRecord = 0;


            var result = new DataSourceResult()
            {
                Data = companyService.GetAllFiltered(filterCompanyName, filterInsertDateFrom, filterInsertDateTo, currentPage, pageSize, out totalRecord),
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
        public IActionResult Create(CompanyViewModel model, IFormFile CompanyLogoFile)
        {
            if (ModelState.IsValid)
            {
                if (!companyService.IsDuplicateByCompanyName(null, model.CompanyName))
                {
                    int companyId = companyService.Add(model);
                    if (companyId != -1)
                    {
                        string directoryPath = webHostEnvironment.WebRootPath + "\\Upload\\CompanyDocument\\" + companyId.ToString();

                        if (Directory.Exists(directoryPath))
                        {
                            Directory.Delete(directoryPath, true);
                        }

                        Directory.CreateDirectory(directoryPath);

                        if (CompanyLogoFile != null && CompanyLogoFile.Length > 0)
                        {
                            string uploadedFileName = CompanyLogoFile.FileName;
                            string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                            string uploadedFileExtention = CompanyLogoFile.FileName.Split('.')[splittedFileName.Length - 1];
                            string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                            string filePath = directoryPath + "\\" + generatedFileName;

                            while (System.IO.File.Exists(filePath))
                            {
                                generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                filePath = directoryPath + "\\" + generatedFileName;
                            }

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                CompanyLogoFile.CopyTo(stream);

                                companyService.SetCompanyLogoFileName(companyId, generatedFileName);

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
                    ShowDangerToast(null, "یک شرکت دیگر با این نام وجود دارد");
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
            var model = companyService.Get(id);
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CompanyViewModel model, IFormFile CompanyLogoFile)
        {
            if (ModelState.IsValid)
            {
                if (!companyService.IsDuplicateByCompanyName(model.CompanyId, model.CompanyName))
                {
                    if (companyService.Edit(model))
                    {
                        string directoryPath = webHostEnvironment.WebRootPath + "\\Upload\\CompanyDocument\\" + model.CompanyId.ToString();

                        if (Directory.Exists(directoryPath))
                        {
                            Directory.Delete(directoryPath, true);
                        }

                        Directory.CreateDirectory(directoryPath);

                        if (CompanyLogoFile != null && CompanyLogoFile.Length > 0)
                        {
                            string uploadedFileName = CompanyLogoFile.FileName;
                            string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                            string uploadedFileExtention = CompanyLogoFile.FileName.Split('.')[splittedFileName.Length - 1];
                            string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                            string filePath = directoryPath + "\\" + generatedFileName;

                            while (System.IO.File.Exists(filePath))
                            {
                                generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                filePath = directoryPath + "\\" + generatedFileName;
                            }

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                CompanyLogoFile.CopyTo(stream);

                                companyService.SetCompanyLogoFileName(model.CompanyId, generatedFileName);

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
                    ShowDangerToast(null, "یک شرکت دیگر با این نام وجود دارد");
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
            return companyService.Delete(id);
        }

        [Route("Fill_Company_Combo")]
        public virtual JsonResult Fill_Company_Combo()
        {
            var DataList = companyService.GetAll()
                .Select(x =>
                new SelectListItem
                {
                    Text = x.CompanyName,
                    Value = x.CompanyId.ToString()
                });
            return Json(DataList);
        }
    }
}
