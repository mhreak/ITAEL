using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Web.Controllers;
using Web.Model;
using Web.Service;
using Web.Service.Identity.Interface;
using Web.Service.Interface;
using Microsoft.AspNetCore.Http;
using static Kendo.Mvc.UI.UIPrimitives;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class ApplicantController : BaseController
    {
        readonly IUtilService _utilService;
        readonly IApplicantService _applicantService;
        readonly IWebHostEnvironment _webHostEnvironment;
        readonly IApplicationUserManagerService _userManagerService;

        public ApplicantController(
            IUtilService utilService,
            IApplicantService applicantService,
            IWebHostEnvironment webHostEnvironment,
            IApplicationUserManagerService userManagerService)
        {
            _utilService = utilService;
            _applicantService = applicantService;
            _webHostEnvironment = webHostEnvironment;
            _userManagerService = userManagerService;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            string filterFullName, string filterGender, string filterNationalCode, string filterMobile,
            string filterBirthDateTo, string filterBirthDateFrom,
            string filterProvinceId, string filterCityId, string filterStudyFieldId,
            string filterInsertDateFrom, string filterInsertDateTo)
        {
            //Paging and Sorting
            int currentPage = request.Page;
            int pageSize = request.PageSize;
            int totalRecord = 0;


            var result = new DataSourceResult()
            {
                Data = _applicantService.GetAllFiltered(null, null, filterFullName, filterGender, filterNationalCode,
                filterMobile, filterBirthDateFrom, filterBirthDateTo,
                filterProvinceId, filterCityId, filterStudyFieldId,
                filterInsertDateFrom, filterInsertDateTo,
                currentPage, pageSize, out totalRecord),
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
        public virtual ActionResult Create(ApplicantViewModel model,
            IFormFile PersonalImageFile, IFormFile EducationalCertificateFile,
            IFormFile NationalCardFrontFile, IFormFile NationalCardBackFile,
            IFormFile IdentityCertificateFirstPageFile, IFormFile IdentityCertificateSecondPageFile
            )
        {
            if (ModelState.IsValid)
            {
                if (!_applicantService.IsDuplicateByNationalCode(null, model.NationalCode))
                {
                    if (!_applicantService.IsDuplicateByMobile(null, model.Mobile))
                    {
                        int applicantId = _applicantService.Add(model);
                        if (applicantId != -1)
                        {
                            string directoryPath = _webHostEnvironment.WebRootPath + "\\Upload\\ApplicantDocument\\" + applicantId.ToString();

                            if (Directory.Exists(directoryPath))
                            {
                                Directory.Delete(directoryPath);
                            }

                            Directory.CreateDirectory(directoryPath);

                            if (PersonalImageFile != null && PersonalImageFile.Length > 0)
                            {
                                string uploadedFileName = PersonalImageFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = PersonalImageFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetPersonalImageFileName(applicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (EducationalCertificateFile != null && EducationalCertificateFile.Length > 0)
                            {
                                string uploadedFileName = EducationalCertificateFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = EducationalCertificateFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetEducationalCertificateFileName(applicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (NationalCardFrontFile != null && NationalCardFrontFile.Length > 0)
                            {
                                string uploadedFileName = NationalCardFrontFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = NationalCardFrontFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetNationalCardFrontFileName(applicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (NationalCardBackFile != null && NationalCardBackFile.Length > 0)
                            {
                                string uploadedFileName = NationalCardBackFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = NationalCardBackFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetNationalCardBackFileName(applicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (IdentityCertificateFirstPageFile != null && IdentityCertificateFirstPageFile.Length > 0)
                            {
                                string uploadedFileName = IdentityCertificateFirstPageFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = IdentityCertificateFirstPageFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetIdentityCertificateFirstPageFileName(applicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (IdentityCertificateSecondPageFile != null && IdentityCertificateSecondPageFile.Length > 0)
                            {
                                string uploadedFileName = IdentityCertificateSecondPageFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = IdentityCertificateSecondPageFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetIdentityCertificateSecondPageFileName(applicantId, generatedFileName);

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
                        ShowDangerToast(null, "یک داوطلب دیگر با این تلفن همراه ثبت شده است");
                    }
                }
                else
                {
                    ShowDangerToast(null, "یک داوطلب دیگر با این کد ملی ثبت شده است");
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
            var model = _applicantService.Get(id);

            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(ApplicantViewModel model,
            IFormFile PersonalImageFile, IFormFile EducationalCertificateFile,
            IFormFile NationalCardFrontFile, IFormFile NationalCardBackFile,
            IFormFile IdentityCertificateFirstPageFile, IFormFile IdentityCertificateSecondPageFile)
        {
            if (ModelState.IsValid)
            {
                if (!_applicantService.IsDuplicateByNationalCode(model.ApplicantId, model.NationalCode))
                {
                    if (!_applicantService.IsDuplicateByMobile(model.ApplicantId, model.Mobile))
                    {
                        if (_applicantService.Edit(model))
                        {
                            string directoryPath = _webHostEnvironment.WebRootPath + "\\Upload\\ApplicantDocument\\" + model.ApplicantId.ToString();

                            if (Directory.Exists(directoryPath))
                            {
                                Directory.Delete(directoryPath);
                            }

                            Directory.CreateDirectory(directoryPath);

                            if (PersonalImageFile != null && PersonalImageFile.Length > 0)
                            {
                                string uploadedFileName = PersonalImageFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = PersonalImageFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetPersonalImageFileName(model.ApplicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (EducationalCertificateFile != null && EducationalCertificateFile.Length > 0)
                            {
                                string uploadedFileName = EducationalCertificateFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = EducationalCertificateFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetEducationalCertificateFileName(model.ApplicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (NationalCardFrontFile != null && NationalCardFrontFile.Length > 0)
                            {
                                string uploadedFileName = NationalCardFrontFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = NationalCardFrontFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetNationalCardFrontFileName(model.ApplicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (NationalCardBackFile != null && NationalCardBackFile.Length > 0)
                            {
                                string uploadedFileName = NationalCardBackFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = NationalCardBackFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetNationalCardBackFileName(model.ApplicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (IdentityCertificateFirstPageFile != null && IdentityCertificateFirstPageFile.Length > 0)
                            {
                                string uploadedFileName = IdentityCertificateFirstPageFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = IdentityCertificateFirstPageFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetIdentityCertificateFirstPageFileName(model.ApplicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (IdentityCertificateSecondPageFile != null && IdentityCertificateSecondPageFile.Length > 0)
                            {
                                string uploadedFileName = IdentityCertificateSecondPageFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = IdentityCertificateSecondPageFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = _utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    _applicantService.SetIdentityCertificateSecondPageFileName(model.ApplicantId, generatedFileName);

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
                        ShowDangerToast(null, "یک داوطلب دیگر با این تلفن همراه ثبت شده است");
                    }
                }
                else
                {
                    ShowDangerToast(null, "یک داوطلب دیگر با این کد ملی ثبت شده است");
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
            return _applicantService.Delete(id);
        }
    }
}
