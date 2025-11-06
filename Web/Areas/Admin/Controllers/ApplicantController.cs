using Web.Model;
using System.IO;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Web.Service.Identity.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class ApplicantController(
        IUtilService utilService,
        IApplicantService applicantService,
        IWebHostEnvironment webHostEnvironment,
        IApplicationUserManagerService userManagerService)
        : BaseController
    {

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
                Data = applicantService.GetAllFiltered(null, null, filterFullName, filterGender, filterNationalCode,
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
                if (!applicantService.IsDuplicateByNationalCode(null, model.NationalCode))
                {
                    if (!applicantService.IsDuplicateByMobile(null, model.Mobile))
                    {
                        int applicantId = applicantService.Add(model);
                        if (applicantId != -1)
                        {
                            string directoryPath = webHostEnvironment.WebRootPath + "\\Upload\\ApplicantDocument\\" + applicantId.ToString();

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
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetPersonalImageFileName(applicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (EducationalCertificateFile != null && EducationalCertificateFile.Length > 0)
                            {
                                string uploadedFileName = EducationalCertificateFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = EducationalCertificateFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetEducationalCertificateFileName(applicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (NationalCardFrontFile != null && NationalCardFrontFile.Length > 0)
                            {
                                string uploadedFileName = NationalCardFrontFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = NationalCardFrontFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetNationalCardFrontFileName(applicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (NationalCardBackFile != null && NationalCardBackFile.Length > 0)
                            {
                                string uploadedFileName = NationalCardBackFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = NationalCardBackFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetNationalCardBackFileName(applicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (IdentityCertificateFirstPageFile != null && IdentityCertificateFirstPageFile.Length > 0)
                            {
                                string uploadedFileName = IdentityCertificateFirstPageFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = IdentityCertificateFirstPageFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetIdentityCertificateFirstPageFileName(applicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (IdentityCertificateSecondPageFile != null && IdentityCertificateSecondPageFile.Length > 0)
                            {
                                string uploadedFileName = IdentityCertificateSecondPageFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = IdentityCertificateSecondPageFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetIdentityCertificateSecondPageFileName(applicantId, generatedFileName);

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
            var model = applicantService.Get(id);

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
                if (!applicantService.IsDuplicateByNationalCode(model.ApplicantId, model.NationalCode))
                {
                    if (!applicantService.IsDuplicateByMobile(model.ApplicantId, model.Mobile))
                    {
                        if (applicantService.Edit(model))
                        {
                            string directoryPath = webHostEnvironment.WebRootPath + "\\Upload\\ApplicantDocument\\" + model.ApplicantId.ToString();

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
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetPersonalImageFileName(model.ApplicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (EducationalCertificateFile != null && EducationalCertificateFile.Length > 0)
                            {
                                string uploadedFileName = EducationalCertificateFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = EducationalCertificateFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetEducationalCertificateFileName(model.ApplicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (NationalCardFrontFile != null && NationalCardFrontFile.Length > 0)
                            {
                                string uploadedFileName = NationalCardFrontFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = NationalCardFrontFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetNationalCardFrontFileName(model.ApplicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (NationalCardBackFile != null && NationalCardBackFile.Length > 0)
                            {
                                string uploadedFileName = NationalCardBackFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = NationalCardBackFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetNationalCardBackFileName(model.ApplicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (IdentityCertificateFirstPageFile != null && IdentityCertificateFirstPageFile.Length > 0)
                            {
                                string uploadedFileName = IdentityCertificateFirstPageFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = IdentityCertificateFirstPageFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetIdentityCertificateFirstPageFileName(model.ApplicantId, generatedFileName);

                                    stream.Close();
                                }
                            }

                            if (IdentityCertificateSecondPageFile != null && IdentityCertificateSecondPageFile.Length > 0)
                            {
                                string uploadedFileName = IdentityCertificateSecondPageFile.FileName;
                                string[] splittedFileName = uploadedFileName.Split('.').ToArray();
                                string uploadedFileExtention = IdentityCertificateSecondPageFile.FileName.Split('.')[splittedFileName.Length - 1];
                                string generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;

                                string filePath = directoryPath + "\\" + generatedFileName;

                                while (System.IO.File.Exists(filePath))
                                {
                                    generatedFileName = utilService.GenerateRandomString(15) + "." + uploadedFileExtention;
                                    filePath = directoryPath + "\\" + generatedFileName;
                                }

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    PersonalImageFile.CopyTo(stream);

                                    applicantService.SetIdentityCertificateSecondPageFileName(model.ApplicantId, generatedFileName);

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
            return applicantService.Delete(id);
        }
    }
}
