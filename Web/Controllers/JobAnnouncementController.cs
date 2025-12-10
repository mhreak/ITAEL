using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using Shodamad.Model.OnlinePayment;
using Shodamad.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Model;
using Web.Model.OnlinePayment;
using Web.Service;
using Web.Service.Identity.Interface;
using Web.Service.Interface;

namespace Web.Controllers
{
    [Authorize(Roles = "Applicant")]
    public class JobAnnouncementController(IJobAnnouncementService jobAnnouncementService,
                                           IJobAnnouncement_JobAnnouncementCategory_Service ja_JaCategory_Service,
                                           IJobAnnouncement_StudyField_Service studyFieldService,
                                           IApplicationUserManagerService applicationUserManagerService,
                                           IApplicant_JobAnnouncement_Service applicant_Ja_Service,
                                           IJobAnnouncement_Skill_Service skillService,
                                           IApplicantService applicantService,
                                           IBankGatewayService bankGatewayService,
                                           IOnlineTransactionService onlineTransactionService,
                                           ICompanyService companyService,
                                           IProvinceService provinceService,
                                           ICityService cityService,
                                           ISettingService settingService)
        : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int id)
        {
            JobAnnouncementDetailsViewModel jobAnnouncementDetailsViewModel = new();

            JobAnnouncementViewModel jobAnnouncement = jobAnnouncementService.Get(id);
            jobAnnouncementDetailsViewModel.JobAnnouncementViewModel = jobAnnouncement;

            jobAnnouncementDetailsViewModel.JobAnnouncementSkillViewModelList = skillService.GetAllByJobAnnouncementId(id).ToList();
            jobAnnouncementDetailsViewModel.JobAnnouncementStudyFieldViewModelList = studyFieldService.GetAllByJobAnnouncementId(id).ToList();
            jobAnnouncementDetailsViewModel.JobAnnouncementJobAnnouncementCategoryViewModelList = ja_JaCategory_Service.GetAllByJobAnnouncementId(id).ToList();
            if (jobAnnouncement.ShowCompanyInfo)
            {
                var companyViewModel = companyService.Get(jobAnnouncement.CompanyId);
                jobAnnouncementDetailsViewModel.CompanyViewModel = companyViewModel;
            }

            var cityViewModel = cityService.Get(jobAnnouncement.CityId);
            jobAnnouncement.ProvinceId = cityViewModel.ProvinceId;
            jobAnnouncement.ProvinceName = cityViewModel.ProvinceName;

            if (jobAnnouncement.JobAnnouncementApplicationDeadlineDateTo != null && jobAnnouncement.JobAnnouncementApplicationDeadlineDateTo.HasValue)
            {
                if (jobAnnouncement.JobAnnouncementApplicationDeadlineDateTo <= DateTime.Now)
                {
                    jobAnnouncementDetailsViewModel.IsAllowToSubmitRequest = false;
                }
                else
                {
                    jobAnnouncementDetailsViewModel.IsAllowToSubmitRequest = true;
                }
            }
            else
            {
                jobAnnouncementDetailsViewModel.IsAllowToSubmitRequest = true;
            }
            return View(jobAnnouncementDetailsViewModel);
        }

        [HttpPost]
        public IActionResult SubmitRequest(int id)
        {
            var user = applicationUserManagerService.GetCurrentUser();
            var applicantViewModel = applicantService.Get(user.ApplicantId.Value);
            var jobAnnouncementViewModel = jobAnnouncementService.Get(id);
            bool isDuplicate = applicant_Ja_Service.IsDuplicate(applicantViewModel.ApplicantId, jobAnnouncementViewModel.JobAnnouncementId);

            if (isDuplicate)
            {
                TempData["ErrorMessage"] = "شما قبلا برای این آگهی درخواست داده اید.";
                return RedirectToAction("Details", "JobAnnouncement", new {Area = "", id });
            }

            var applicant_Ja_ViewModel = new Applicant_JobAnnouncement_ViewModel()
                                         {
                                             ApplicantId = applicantViewModel.ApplicantId,
                                             JobAnnouncementId = jobAnnouncementViewModel.JobAnnouncementId,
                                             InsertDate = DateTime.Now,
                                             Status = 0
                                         };

            bool isAdd = applicant_Ja_Service.Add(applicant_Ja_ViewModel);

            if (isAdd)
            {
                if (jobAnnouncementViewModel.Price is not null)
                {
                    TempData["SuccessMessage"] = "درخواست شما با موفقیت ثبت شد به صفحه پرداخت منتقل میشوید.";
                    return RedirectToAction("OnlinePay", "JobAnnouncement", new { Area = "", id });
                }
                else
                {
                    TempData["SuccessMessage"] = "درخواست شما با موفقیت ثبت شد.";
                    return RedirectToAction("Details", "JobAnnouncement", new { Area = "", id });
                }
            }

            TempData["ErrorMessage"] = "درخواست شما ثبت نشد لطفا دوباره تلاش کنید.";
            return RedirectToAction("Details", "JobAnnouncement", new { Area = "", id });
        }

        [Route("OnlinePay/{jobAnnouncementId}")]
        public IActionResult OnlinePay(int jobAnnouncementId)
        {
            var currentUser = applicationUserManagerService.GetCurrentUser();

            if (currentUser != null && currentUser.ApplicantId != null)
            {
                var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

                if (jobAnnouncementViewModel != null)
                {
                    var applicantViewModel = applicantService.Get((int)currentUser.ApplicantId);
                    
                    if (applicantViewModel != null)
                    {
                        var bankGateWay = bankGatewayService.GetAll(true).FirstOrDefault();

                        if (bankGateWay != null)
                        {
                            OnlineTransactionViewModel transaction = new OnlineTransactionViewModel();
                            transaction.ApplicantId = applicantViewModel.ApplicantId;
                            transaction.JobAnnouncementId = jobAnnouncementId;
                            transaction.State = 0;
                            transaction.InsertDate = DateTime.Now;
                            transaction.BankGatewayId = bankGateWay.BankGatewayId;
                            transaction.Amount = (int)jobAnnouncementViewModel.Price;

                            long transactionId = onlineTransactionService.Add(transaction);

                            if (transactionId != -1)
                            {
                                try
                                {

                                    ZarinpalRequestParameters Parameters = new ZarinpalRequestParameters(
                                            bankGateWay.Parameter1,
                                            transaction.Amount.ToString(),
                                            "درخواست برای آگهی " + jobAnnouncementViewModel.Title,
                                            settingService.GetValueByKey("BaseUrl") + "/ExamResource/VerifyOnlinePay/" + transactionId.ToString(),
                                            applicantViewModel.Mobile, "");


                                    //be dalil in ke metadata be sorate araye ast va do meghdare mobile va email dar metadata gharar mmigirad
                                    //shoma mitavanid in maghadir ra az kharidar begirid va set konid dar gheir in sorat khali ersal konid

                                    var client = new RestClient("https://api.zarinpal.com/pg/v4/payment/request.json");

                                    Method method = Method.Post;

                                    var request = new RestRequest("", method);

                                    request.AddHeader("accept", "application/json");

                                    request.AddHeader("content-type", "application/json");

                                    request.AddJsonBody(Parameters);

                                    var requestresponse = client.ExecuteAsync(request);

                                    JObject jo = JObject.Parse(requestresponse.Result.Content);

                                    string errorscode = jo["errors"].ToString();

                                    JObject jodata = JObject.Parse(requestresponse.Result.Content);

                                    string dataauth = jodata["data"].ToString();


                                    if (dataauth != "[]")
                                    {
                                        string authority = jodata["data"]["authority"].ToString();

                                        string gatewayUrl = "https://www.zarinpal.com/pg/StartPay/" + authority;

                                        return Redirect(gatewayUrl);
                                    }
                                    else
                                    {
                                        return BadRequest("error " + errorscode);
                                    }


                                }

                                catch (Exception ex)
                                {
                                    //    throw new Exception(ex.Message);

                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        [HttpGet]
        [Route("VerifyOnlinePay/{id}")]
        public IActionResult VerifyOnlinePay(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = applicationUserManagerService.GetCurrentUser();

                var applicantViewModel = applicantService.Get((int)currentUser.ApplicantId);

                var transaction = onlineTransactionService.Get(id);

                if (transaction != null && transaction.JobAnnouncementId != null && transaction.ApplicantId != null)
                {
                    var jobAnnouncementViewModel = jobAnnouncementService.Get(transaction.JobAnnouncementId);
                    //var examResourceViewModel = examResourceService.Get(examResourceOrderViewModel.ExamResourceId);
                    var bankGateway = bankGatewayService.GetAll(true).FirstOrDefault();

                    if (jobAnnouncementViewModel != null && bankGateway != null)
                    {
                        if (jobAnnouncementViewModel.Price == transaction.Amount)
                        {
                            try
                            {
                                ZarinpalVerifyParameters parameters = new ZarinpalVerifyParameters();
                                string authority = "";

                                if (HttpContext.Request.Query["Authority"] != "")
                                {
                                    authority = HttpContext.Request.Query["Authority"];
                                }

                                parameters.authority = authority;
                                parameters.amount = transaction.Amount.ToString();
                                parameters.merchant_id = bankGateway.Parameter1;

                                var client = new RestClient("https://api.zarinpal.com/pg/v4/payment/verify.json");
                                Method method = Method.Post;
                                var request = new RestRequest("", method);

                                request.AddHeader("accept", "application/json");

                                request.AddHeader("content-type", "application/json");
                                request.AddJsonBody(parameters);

                                var response = client.ExecuteAsync(request);


                                JObject jodata = JObject.Parse(response.Result.Content);

                                string data = jodata["data"].ToString();

                                JObject jo = JObject.Parse(response.Result.Content);

                                string errors = jo["errors"].ToString();

                                if (data != "[]")
                                {
                                    string refid = jodata["data"]["ref_id"].ToString();

                                    onlineTransactionService.SetReferenceNumber(id, refid);

                                    if (onlineTransactionService.SetState(id, 1, DateTime.Now))
                                    {
                                        var applicant_ja_ViewModel = applicant_Ja_Service.Get(applicantViewModel.ApplicantId, jobAnnouncementViewModel.JobAnnouncementId);
                                        applicant_ja_ViewModel.Status = 1;
                                        bool isEdit = applicant_Ja_Service.Edit(applicant_ja_ViewModel);
                                        if (isEdit)
                                        {
                                            return RedirectToAction("OnlinePaymentResult", new { Area = "Applicant", resCode = 1, transactionId = id });
                                        }
                                        else
                                        {
                                            onlineTransactionService.SetState(id, -1, null);

                                            return RedirectToAction("OnlinePaymentResult", new { Area = "Applicant", resCode = -2, transactionId = id });
                                        }
                                    }
                                    else
                                    {
                                        return RedirectToAction("OnlinePaymentResult", new { Area = "Applicant", resCode = -3, transactionId = id });
                                    }
                                }
                                else if (errors != "[]")
                                {

                                    string errorscode = jo["errors"]["code"].ToString();

                                    return BadRequest($"error code {errorscode}");

                                }
                            }
                            catch (Exception e)
                            {
                            }
                        }
                        else
                        {
                            return RedirectToAction("OnlinePaymentResult", new { Area = "Applicant", resCode = -4, transactionId = id });
                        }
                    }
                    else
                    {
                        return RedirectToAction("OnlinePaymentResult", new { Area = "Applicant", resCode = -5, transactionId = id });
                    }
                }
                else
                {
                    return RedirectToAction("OnlinePaymentResult", new { Area = "Applicant", resCode = -7, transactionId = id });
                }
            }
            else
            {
                return null;
            }
            return null;
        }

        [Route("OnlinePaymentResult/{resCode}/{transactionId}")]
        public IActionResult OnlinePaymentResult(int resCode, long transactionId)
        {
            var currentUser = applicationUserManagerService.GetCurrentUser();
            if (currentUser != null && currentUser.ApplicantId != null)
            {
                var applicantViewModel = applicantService.Get((int)currentUser.ApplicantId);
                if (applicantViewModel != null)
                {
                    var transaction = onlineTransactionService.Get(transactionId);

                    if (transaction != null && transaction.JobAnnouncementId != null)
                    {
                        var jobAnnouncementViewModel = jobAnnouncementService.Get(transaction.JobAnnouncementId);

                        if (jobAnnouncementViewModel != null)
                        {
                            if (transaction.ApplicantId == applicantViewModel.ApplicantId)
                            {
                                switch (resCode)
                                {
                                    case 1:
                                        ViewBag.ReferenceNumber = transaction.ReferenceNumber;
                                        break;
                                }

                                return View(jobAnnouncementViewModel);
                            }
                        }
                    }
                }
            }
            return null;
        }
    }

    public class JobAnnouncementDetailsViewModel
    {
        public CompanyViewModel CompanyViewModel { get; set; }
        public JobAnnouncementViewModel JobAnnouncementViewModel { get; set; }
        public List<JobAnnouncement_Skill_ViewModel> JobAnnouncementSkillViewModelList { get; set; }
        public List<JobAnnouncement_StudyField_ViewModel> JobAnnouncementStudyFieldViewModelList { get; set; }
        public List<JobAnnouncement_JobAnnouncementCategory_ViewModel> JobAnnouncementJobAnnouncementCategoryViewModelList { get; set; }

        public bool IsAllowToSubmitRequest { get; set; }
    }
}
