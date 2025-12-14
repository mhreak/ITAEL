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
using Web.Helper;
using Web.Model;
using Web.Service;
using Web.Service.Identity.Interface;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class ApplicantController(
        IUtilService utilService,
        IApplicantService applicantService,
        IWebHostEnvironment webHostEnvironment,
        ICityService cityService,
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
        public IActionResult Create(ApplicantViewModel model,
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
                        var cityList = cityService.GetAllByProvinceId(model.ProvinceId.Value);
                        model.CityId = cityList.First().CityId;
                        int applicantId = applicantService.Add(model, PasswordGenerator.GenerateIdentityPassword());
                        if (applicantId != -1)
                        {
                            string directoryPath = webHostEnvironment.WebRootPath + "\\Upload\\ApplicantDocument\\" + applicantId.ToString();

                            if (Directory.Exists(directoryPath))
                            {
                                Directory.Delete(directoryPath, true);
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
            var applicantViewModel = applicantService.Get(id);
            
            if (applicantViewModel == null)
            {
                ShowDangerToast("", "داوطلب یافت نشد.");
                return RedirectToAction("Index", "Applicant", new {Area = "Admin"});
            }

            var model = new UpdateProfileViewModel();

            model.ApplicantViewModel = applicantViewModel;
            
            var baseUrl = $"/Upload/ApplicantDocument/{applicantViewModel.ApplicantId}/";

            model.PersonalImageFileUrl =
                BuildFileUrl(baseUrl, applicantViewModel.PersonalImageFileName);

            model.EducationalCertificateFileUrl =
                BuildFileUrl(baseUrl, applicantViewModel.EducationalCertificateFileName);

            model.NationalCardFrontFileUrl =
                BuildFileUrl(baseUrl, applicantViewModel.NationalCardFrontFileName);

            model.NationalCardBackFileUrl =
                BuildFileUrl(baseUrl, applicantViewModel.NationalCardBackFileName);

            model.IdentityCertificateFirstPageFileUrl =
                BuildFileUrl(baseUrl, applicantViewModel.IdentityCertificateFirstPageFileName);

            model.IdentityCertificateSecondPageFileUrl =
                BuildFileUrl(baseUrl, applicantViewModel.IdentityCertificateSecondPageFileName);

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateProfileViewModel model,
                                  IFormFile PersonalImageFile, IFormFile EducationalCertificateFile,
                                  IFormFile NationalCardFrontFile, IFormFile NationalCardBackFile,
                                  IFormFile IdentityCertificateFirstPageFile, IFormFile IdentityCertificateSecondPageFile)
        {
            if (ModelState.IsValid)
            {
                if (!applicantService.IsDuplicateByNationalCode(model.ApplicantViewModel.ApplicantId, model.ApplicantViewModel.NationalCode))
                {
                    if (!applicantService.IsDuplicateByMobile(model.ApplicantViewModel.ApplicantId, model.ApplicantViewModel.Mobile))
                    {
                        if (applicantService.Edit(model.ApplicantViewModel))
                        {
                            string directoryPath = webHostEnvironment.WebRootPath + "\\Upload\\ApplicantDocument\\" + model.ApplicantViewModel.ApplicantId.ToString();

                            if (Directory.Exists(directoryPath))
                            {
                                Directory.Delete(directoryPath, true);
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

                                    applicantService.SetPersonalImageFileName(model.ApplicantViewModel.ApplicantId, generatedFileName);

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

                                    applicantService.SetEducationalCertificateFileName(model.ApplicantViewModel.ApplicantId, generatedFileName);

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

                                    applicantService.SetNationalCardFrontFileName(model.ApplicantViewModel.ApplicantId, generatedFileName);

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

                                    applicantService.SetNationalCardBackFileName(model.ApplicantViewModel.ApplicantId, generatedFileName);

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

                                    applicantService.SetIdentityCertificateFirstPageFileName(model.ApplicantViewModel.ApplicantId, generatedFileName);

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

                                    applicantService.SetIdentityCertificateSecondPageFileName(model.ApplicantViewModel.ApplicantId, generatedFileName);

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

        [Route("Fill_Applicant_Combo")]
        public IActionResult Fill_Applicant_Combo()
        {
            var DataList = applicantService.GetAll()
                                      .Select(x =>
                                                  new SelectListItem
                                                  {
                                                      Text = x.FullName,
                                                      Value = x.ApplicantId.ToString()
                                                  });
            return Json(DataList);
        }

        [Route("ShowOperationMenuDrawer/{applicantId}")]
        public virtual ActionResult ShowOperationMenuDrawer(int applicantId)
        {
            var model = applicantService.Get(applicantId);

            return PartialView("_ApplicantOprationDrawer", model);
        }

        [Route("ShowApplicantSearchDialog")]
        public virtual ActionResult ShowApplicantSearchDialog(string valueElementId, string displayElementId)
        {
            ViewBag.ValueElementId = valueElementId;
            ViewBag.DisplayElementId = displayElementId;

            return PartialView("_ApplicantSearchDialog");
        }

        [Route("ShowApplicantsSearchDialog")]
        public virtual ActionResult ShowApplicantsSearchDialog(string valueElementId, string displayElementId)
        {
            ViewBag.ValueElementId = valueElementId;
            ViewBag.DisplayElementId = displayElementId;

            return PartialView("_ApplicantsSearchDialog");
        }

        private static string BuildFileUrl(string baseUrl, string fileName)
        {
            return string.IsNullOrWhiteSpace(fileName)
                       ? string.Empty
                       : baseUrl + fileName;
        }
    }
}
