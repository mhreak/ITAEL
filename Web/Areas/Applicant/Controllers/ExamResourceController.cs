using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json.Linq;
using Shodamad.Service;
using System;
using System.Collections.Generic;
using System.Linq;

using Shodamad.Model.OnlinePayment;

using Web.Model;
using Web.Service;
using Web.Service.Identity.Interface;
using Web.Service.Interface;
using RestSharp;

using Web.Model.OnlinePayment;

using Method = RestSharp.Method;

namespace Web.Areas.Applicant.Controllers
{
    [Area("Applicant")]
    [Route("Applicant/[controller]")]
    [Authorize(Roles = "Applicant")]
    public class ExamResourceController(IExamResourceService examResourceService,
                                        IApplicantService applicantService,
                                        IBankGatewayService bankGatewayService,
                                        IExamResourceOrderItemService examResourceOrderItemService,
                                        IOnlineTransactionService onlineTransactionService,
                                        IJobAnnouncementService jobAnnouncementService,
                                        IApplicationUserManagerService applicationUserManagerService,
                                        IExamResourceOrderService examResourceOrderService,
                                        IJobAnnouncement_ExamResource_Service ja_examResource,
                                        ISettingService settingService) : Controller
    {
        [Route("Index/{jobAnnouncementId}")]
        public IActionResult Index(int jobAnnouncementId)
        {
            var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

            if (jobAnnouncementViewModel == null)
            {
                TempData["ErrorMessage"] = "آگهی یافت نشد.";
            }

            ShowExamResourceViewModel showExamResourceViewModel = new();

            var ja_examResourceViewModelList = ja_examResource.GetAllByJobAnnouncementId(jobAnnouncementId);
            var examResourceViewModelList = new List<ExamResourceViewModel>();
            foreach (var ja_examResourceViewModel in ja_examResourceViewModelList)
            {
                var examResourceViewModel = examResourceService.Get(ja_examResourceViewModel.ExamResourceId);
                examResourceViewModelList.Add(examResourceViewModel);
            }

            showExamResourceViewModel.ExamResourceViewModelList = examResourceViewModelList;
            showExamResourceViewModel.JobAnnouncementViewModel = jobAnnouncementViewModel;
            return View(showExamResourceViewModel);
        }

        [HttpGet]
        [Route("Order/{jobAnnouncementId}")]
        public IActionResult Order(int jobAnnouncementId)
        {
            var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

            if (jobAnnouncementViewModel == null)
            {
                TempData["ErrorMessage"] = "آگهی یافت نشد.";
            }

            ShowExamResourceViewModel showExamResourceViewModel = new();

            var ja_examResourceViewModelList = ja_examResource.GetAllByJobAnnouncementId(jobAnnouncementId);
            var examResourceViewModelList = new List<ExamResourceViewModel>();
            foreach (var ja_examResourceViewModel in ja_examResourceViewModelList)
            {
                var examResourceViewModel = examResourceService.Get(ja_examResourceViewModel.ExamResourceId);

                if (examResourceViewModel.Type == 2 && (examResourceViewModel.Price == null || !examResourceViewModel.Price.HasValue))
                {
                    continue;
                }

                examResourceViewModelList.Add(examResourceViewModel);
            }

            showExamResourceViewModel.ExamResourceViewModelList = examResourceViewModelList;
            showExamResourceViewModel.JobAnnouncementViewModel = jobAnnouncementViewModel;
            return View(showExamResourceViewModel);
        }

        [HttpPost]
        [Route("Order/{jobAnnouncementId}")]
        public IActionResult Order(int jobAnnouncementId, int[] selectedIds)
        {
            var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

            if (jobAnnouncementViewModel == null)
            {
                TempData["ErrorMessage"] = "آگهی یافت نشد.";
            }

            if (selectedIds == null || selectedIds.Length == 0)
            {
                TempData["ErrorMessage"] = "هیچ موردی انتخاب نشده است.";
                var vm = new ShowExamResourceViewModel
                {
                    JobAnnouncementViewModel = jobAnnouncementViewModel,
                    ExamResourceViewModelList = ja_examResource.GetAllByJobAnnouncementId(jobAnnouncementId)
                                                                                                            .Select(j => examResourceService.Get(j.ExamResourceId))
                                                                                                            .Where(e => e != null)
                                                                                                            .ToList()
                };
                return View(vm);
            }

            var user = applicationUserManagerService.GetCurrentUser();
            var applicantViewModel = applicantService.Get(user.ApplicantId.Value);

            var examResourceOrderViewModel = new ExamResourceOrderViewModel()
            {
                ApplicantId = applicantViewModel.ApplicantId,
                Status = 1,
                TotalPrice = 0,
                OrderDate = DateTime.Now
            };
            int examResourceOrderId = examResourceOrderService.Add(examResourceOrderViewModel);

            if (examResourceOrderId == -1)
            {
                TempData["ErrorMessage"] = "لطفا دوباره تلاش کنید.";
                var vm = new ShowExamResourceViewModel
                {
                    JobAnnouncementViewModel = jobAnnouncementViewModel,
                    ExamResourceViewModelList = ja_examResource.GetAllByJobAnnouncementId(jobAnnouncementId)
                                                                        .Select(j => examResourceService.Get(j.ExamResourceId))
                                                                        .Where(e => e != null)
                                                                        .ToList()
                };
                return View(vm);
            }

            var examResourceViewModelList = new List<ExamResourceViewModel>();
            foreach (var id in selectedIds)
            {
                var examResourceViewModel = examResourceService.Get(id);

                if (examResourceViewModel == null)
                {
                    TempData["ErrorMessage"] = "لطفا دوباره تلاش کنید.";
                    var vm = new ShowExamResourceViewModel
                    {
                        JobAnnouncementViewModel = jobAnnouncementViewModel,
                        ExamResourceViewModelList = ja_examResource.GetAllByJobAnnouncementId(jobAnnouncementId)
                                                                            .Select(j => examResourceService.Get(j.ExamResourceId))
                                                                            .Where(e => e != null)
                                                                            .ToList()
                    };
                    return View(vm);
                }

                var examResourceOrderItemViewModel = new ExamResourceOrderItemViewModel()
                {
                    Price = examResourceViewModel.Price ?? 0,
                    InsertDate = DateTime.Now,
                    ExamResourceId = examResourceViewModel.ExamResourceId,
                    ExamResourceOrderId = examResourceOrderId
                };

                int examResourceOrderItemId = examResourceOrderItemService.Add(examResourceOrderItemViewModel);
            }

            return RedirectToAction("ShoppingCart", "ExamResource", new { Area = "Applicant", examResourceOrderId, jobAnnouncementId });

        }

        [HttpGet]
        [Route("ShoppingCart/{jobAnnouncementId}/{examResourceOrderId}")]
        public IActionResult ShoppingCart(int jobAnnouncementId, int examResourceOrderId)
        {
            var user = applicationUserManagerService.GetCurrentUser();

            var applicantViewModel = applicantService.Get(user.ApplicantId.Value);

            var examResourceOrderViewModel = examResourceOrderService.Get(examResourceOrderId);

            decimal totalPrice = 0;
            var examResourceViewModelList = new List<ExamResourceViewModel>();
            foreach (var examResourceOrderItemViewModel in examResourceOrderViewModel.ExamResourceOrderItemViewModelList)
            {
                totalPrice += examResourceOrderItemViewModel.Price;
                var examResourceViewModel = examResourceService.Get(examResourceOrderItemViewModel.ExamResourceId);
                examResourceViewModelList.Add(examResourceViewModel);
            }

            examResourceOrderViewModel.TotalPrice = totalPrice;
            examResourceOrderService.Edit(examResourceOrderViewModel);

            var userProfileForOrderViewModel = new UserProfileForOrderViewModel()
            {
                ApplicantViewModel = applicantViewModel,
                ExamResourceViewModelList = examResourceViewModelList,
                TotalPrice = totalPrice
            };
            ViewBag.ExamResourceOrderId = examResourceOrderId;
            ViewBag.JobAnnouncementId = jobAnnouncementId;
            return View(userProfileForOrderViewModel);
        }

        [HttpPost]
        [Route("ShoppingCart/{jobAnnouncementId}/{examResourceOrderId}")]
        public IActionResult ShoppingCart(UserProfileForOrderViewModel model, int jobAnnouncementId, int examResourceOrderId)
        {
            var isEdit = applicantService.Edit(model.ApplicantViewModel);

            if (isEdit)
            {
                var examResourceOrderViewModel = examResourceOrderService.Get(examResourceOrderId);

                examResourceOrderViewModel.Status = 2;
                examResourceOrderService.Edit(examResourceOrderViewModel);
                return RedirectToAction("OnlinePay", "ExamResource", new { Area = "Applicant", examResourceOrderId = examResourceOrderViewModel.ExamResourceOrderId, jobAnnouncementId = jobAnnouncementId });
            }

            return RedirectToAction();
        }

        [Route("OnlinePay/{examResourceOrderId}/{jobAnnouncementId}")]
        public IActionResult OnlinePay(int examResourceOrderId, int jobAnnouncementId)
        {
            var currentUser = applicationUserManagerService.GetCurrentUser();

            if (currentUser != null && currentUser.ApplicantId != null)
            {
                var examResourceOrderViewModel = examResourceOrderService.Get(examResourceOrderId);

                var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

                if (examResourceOrderViewModel != null && jobAnnouncementViewModel != null)
                {
                    var applicantViewModel = applicantService.Get((int)currentUser.ApplicantId);
                    //var examResourceViewModel = examResourceService.Get(examResourceOrderViewModel.ExamResourceId);
                    if (applicantViewModel != null)
                    {
                        var bankGateWay = bankGatewayService.GetAll(true).FirstOrDefault();

                        if (bankGateWay != null)
                        {
                            OnlineTransactionViewModel transaction = new OnlineTransactionViewModel();
                            transaction.ExamResourceOrderId = examResourceOrderId;
                            transaction.ApplicantId = applicantViewModel.ApplicantId;
                            transaction.JobAnnouncementId = jobAnnouncementId;
                            transaction.State = 0;
                            transaction.InsertDate = DateTime.Now;
                            transaction.BankGatewayId = bankGateWay.BankGatewayId;
                            transaction.Amount = (int)examResourceOrderViewModel.TotalPrice;

                            long transactionId = onlineTransactionService.Add(transaction);

                            if (transactionId != -1)
                            {
                                try
                                {

                                    ZarinpalRequestParameters Parameters = new ZarinpalRequestParameters(
                                            bankGateWay.Parameter1,
                                            transaction.Amount.ToString(),
                                            "خرید منابع آموزشی " + examResourceOrderViewModel.ExamResourceOrderItemViewModelList.Select(x => x.ResourceName).ToString().Split(','),
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

                if (transaction != null && transaction.ExamResourceOrderId != null)
                {
                    var examResourceOrderViewModel = examResourceOrderService.Get(transaction.ExamResourceOrderId.Value);
                    //var examResourceViewModel = examResourceService.Get(examResourceOrderViewModel.ExamResourceId);
                    var bankGateway = bankGatewayService.GetAll(true).FirstOrDefault();

                    if (examResourceOrderViewModel != null && bankGateway != null)
                    {
                        if (examResourceOrderViewModel.TotalPrice == transaction.Amount)
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
                                        if (examResourceOrderService.SetStatus(examResourceOrderViewModel.ExamResourceOrderId, 2))
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

                    if (transaction != null && transaction.ExamResourceOrderId != null)
                    {
                        var examResourceOrderViewModel = examResourceOrderService.Get(transaction.ExamResourceOrderId.Value);

                        if (examResourceOrderViewModel != null)
                        {
                            if (examResourceOrderViewModel.ApplicantId == applicantViewModel.ApplicantId)
                            {
                                switch (resCode)
                                {
                                    case 1:
                                        ViewBag.ReferenceNumber = transaction.ReferenceNumber;
                                        break;
                                }

                                return View(examResourceOrderViewModel);
                            }
                        }
                    }
                }
            }
            return null;
        }
    }

    public class ShowExamResourceViewModel
    {
        public JobAnnouncementViewModel JobAnnouncementViewModel { get; set; }
        public List<ExamResourceViewModel> ExamResourceViewModelList { get; set; }

    }

    public class PaymentViewModel
    {
        public ExamResourceViewModel ExamResourceViewModel { get; set; }
    }

    public class UserProfileForOrderViewModel
    {
        public ApplicantViewModel ApplicantViewModel { get; set; }

        public List<ExamResourceViewModel> ExamResourceViewModelList { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
