using System;
using Web.Model;
using System.IO;
using AutoMapper;
using Web.Controllers;
using DbEntities.Identity;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Web.Service.Identity.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Applicant.Controllers;

[Area("Applicant")]
[Route("Applicant/[controller]")]
[Authorize(Roles = "Applicant,Admin")]
public class ProfileController(IApplicantService applicantService,
                               IApplicationUserManagerService applicationUserManagerService,
                               IWebHostEnvironment webHostEnvironment,
                               IUtilService utilService,
                               IMapper mapper) : BaseController
{
    [HttpGet]
    [Route("UpdateProfile")]
    public IActionResult UpdateProfile()
    {
        var user = applicationUserManagerService.GetCurrentUser();
        if (user == null || !user.ApplicantId.HasValue)
            return NotFound();

        var applicant = applicantService.Get(user.ApplicantId.Value);
        if (applicant == null)
            return NotFound();

        var model = new UpdateProfileViewModel
                    {
                        ApplicantViewModel = applicant
                    };

        var baseUrl = $"/Upload/ApplicantDocument/{applicant.ApplicantId}/";

        model.PersonalImageFileUrl =
            BuildFileUrl(baseUrl, applicant.PersonalImageFileName);

        model.EducationalCertificateFileUrl =
            BuildFileUrl(baseUrl, applicant.EducationalCertificateFileName);

        model.NationalCardFrontFileUrl =
            BuildFileUrl(baseUrl, applicant.NationalCardFrontFileName);

        model.NationalCardBackFileUrl =
            BuildFileUrl(baseUrl, applicant.NationalCardBackFileName);

        model.IdentityCertificateFirstPageFileUrl =
            BuildFileUrl(baseUrl, applicant.IdentityCertificateFirstPageFileName);

        model.IdentityCertificateSecondPageFileUrl =
            BuildFileUrl(baseUrl, applicant.IdentityCertificateSecondPageFileName);

        return View(model);
    }

    [HttpPost]
    [Route("UpdateProfile")]
    public IActionResult UpdateProfile(UpdateProfileViewModel model,
                                       IFormFile PersonalImageFile, IFormFile EducationalCertificateFile,
                                       IFormFile NationalCardFrontFile, IFormFile NationalCardBackFile,
                                       IFormFile IdentityCertificateFirstPageFile, IFormFile IdentityCertificateSecondPageFile)
    {
        var user = applicationUserManagerService.GetCurrentUser();
        if (user == null || !user.ApplicantId.HasValue)
        {
            return NotFound();
        }

        var applicantViewModel = applicantService.Get(user.ApplicantId.Value);

        if (applicantViewModel == null)
        {
            return NotFound();
        }

        bool mobileChanged = applicantViewModel.Mobile != model.ApplicantViewModel.Mobile;
        bool firstNameChanged = applicantViewModel.FirstName != model.ApplicantViewModel.FirstName;
        bool lastNameChanged = applicantViewModel.LastName != model.ApplicantViewModel.LastName;
        if (!applicantService.IsDuplicateByNationalCode(applicantViewModel.ApplicantId, model.ApplicantViewModel.NationalCode))
        {
            if (!applicantService.IsDuplicateByMobile(applicantViewModel.ApplicantId, model.ApplicantViewModel.Mobile))
            {
                bool isEdit = applicantService.Edit(model.ApplicantViewModel);

                if (!isEdit)
                {
                    TempData["ErrorMessage"] = "خطا در ویرایش اطلاعات.";
                    return RedirectToAction("UpdateProfile", "Profile", new { Area = "Applicant" });
                }

                if (mobileChanged || firstNameChanged || lastNameChanged)
                {
                    var applicationUser = new ApplicationUser();

                    user.PhoneNumber = model.ApplicantViewModel.Mobile;
                    user.Name = model.ApplicantViewModel.LastName + " " + model.ApplicantViewModel.LastName;

                    mapper.Map(user, applicationUser);

                    applicationUserManagerService.Edit(applicationUser);
                }

                string directoryPath = webHostEnvironment.WebRootPath + "\\Upload\\ApplicantDocument\\" + applicantViewModel.ApplicantId.ToString();

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }


                void SaveFile(IFormFile file, Action<int, string> setFileNameAction)
                {
                    if (file == null || file.Length == 0) return;

                    var uploadedFileName = Path.GetFileName(file.FileName);
                    var uploadedFileExtention = Path.GetExtension(uploadedFileName);
                    if (string.IsNullOrEmpty(uploadedFileExtention))
                    {
                        uploadedFileExtention = ".bin";
                    }

                    string generatedFileName;
                    string filePath;
                    do
                    {
                        generatedFileName = utilService.GenerateRandomString(15) + uploadedFileExtention;
                        filePath = Path.Combine(directoryPath, generatedFileName);
                    } while (System.IO.File.Exists(filePath));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        stream.Flush();
                        stream.Close();
                    }

                    setFileNameAction(applicantViewModel.ApplicantId, generatedFileName);
                }

                SaveFile(PersonalImageFile, (id, name) => applicantService.SetPersonalImageFileName(id, name));
                SaveFile(EducationalCertificateFile, (id, name) => applicantService.SetEducationalCertificateFileName(id, name));
                SaveFile(NationalCardFrontFile, (id, name) => applicantService.SetNationalCardFrontFileName(id, name));
                SaveFile(NationalCardBackFile, (id, name) => applicantService.SetNationalCardBackFileName(id, name));
                SaveFile(IdentityCertificateFirstPageFile, (id, name) => applicantService.SetIdentityCertificateFirstPageFileName(id, name));
                SaveFile(IdentityCertificateSecondPageFile, (id, name) => applicantService.SetIdentityCertificateSecondPageFileName(id, name));

                if (mobileChanged)
                {
                    TempData["SuccessMessage"] = "اطلاعات با موفقیت ثبت شد لطفا دوباره وارد شوید.";
                    return RedirectToAction("LogOut", "Account", new { Area = "Applicant" });
                }

                TempData["SuccessMessage"] = "اطلاعات با موفقیت ثبت شد.";
                return RedirectToAction("Index", "Dashboard", new { Area = "Applicant" });
            }
        }
        TempData["ErrorMessage"] = "شماره تلفن یا کدملی قبلا در سامانه ثبت شده است.";
        return RedirectToAction("Index", "Dashboard", new { Area = "Applicant" });
    }

    private static string BuildFileUrl(string baseUrl, string fileName)
    {
        return string.IsNullOrWhiteSpace(fileName)
                   ? string.Empty
                   : baseUrl + fileName;
    }
}