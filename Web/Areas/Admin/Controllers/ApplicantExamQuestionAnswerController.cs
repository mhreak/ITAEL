using DbEntities;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

using Web.Controllers;
using Web.Model;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class ApplicantExamQuestionAnswerController(IExamService examService,
                                                       IApplicantService applicantService,
                                                       IExamQuestionService examQuestionService,
                                                       IJobAnnouncementService jobAnnouncementService,
                                                       IApplicantExamAttemptService applicantExamAttemptService,
                                                       IApplicantExamQuestionAnswerService applicantExamQuestionAnswerService) : BaseController
    {
        [Route("Index/{applicantExamAttemptId}/{jobAnnouncementId}")]
        public IActionResult Index(int applicantExamAttemptId, int jobAnnouncementId)
        {
            var applicantExamAttemptViewModel = applicantExamAttemptService.Get(applicantExamAttemptId);
            var examViewModel = examService.Get(applicantExamAttemptViewModel.ExamId);

            if (examViewModel == null)
            {
                ShowDangerToast(null, "آزمون یافت نشد");
            }

            var applicantViewModel = applicantService.Get(applicantExamAttemptViewModel.ApplicantId);

            if (applicantViewModel == null)
            {
                ShowDangerToast(null, "داوطلب یافت نشد.");
            }

            var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

            if (applicantViewModel == null)
            {
                ShowDangerToast(null, "آگهی یافت نشد.");
            }

            var examQuestionViewModelList = examQuestionService.GetAllByExamId(examViewModel.ExamId);
            var dataList = new List<ApplicantExamQuestionAndAnswerForAdminViewModel>();
            foreach (var examQuestionViewModel in examQuestionViewModelList)
            {
                var applicantExamQuestionAnswerViewModel = applicantExamQuestionAnswerService.Get(applicantExamAttemptViewModel.ApplicantExamAttemptId,
                                                                                                  examQuestionViewModel.ExamQuestionId);

                var data = new ApplicantExamQuestionAndAnswerForAdminViewModel()
                {
                    ApplicantExamQuestionAnswerViewModel = applicantExamQuestionAnswerViewModel,
                    ExamQuestionViewModel = examQuestionViewModel
                };

                dataList.Add(data);
            }

            ViewBag.ExamId = examViewModel.ExamId;
            ViewBag.ExamTitle = examViewModel.Title;
            ViewBag.ApplicantId = applicantViewModel.ApplicantId;
            ViewBag.ApplicantFullName = applicantViewModel.FullName;
            ViewBag.JobAnnouncementId = jobAnnouncementViewModel.JobAnnouncementId;
            ViewBag.JobAnnouncementTitle = jobAnnouncementViewModel.Title;
            ViewBag.ApplicantExamAttemptId = applicantExamAttemptViewModel.ApplicantExamAttemptId;

            return View(dataList);
        }

        [HttpPost]
        [Route("Update/{applicantExamAttemptId}/{jobAnnouncementId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int applicantExamAttemptId, int jobAnnouncementId, [FromBody] CorrectionRequestDto model)
        {
            if (model == null || model.Corrections == null)
            {
                return BadRequest(new { success = false, message = "درخواست نامعتبر است." });
            }

            if (model.ApplicantExamAttemptId != applicantExamAttemptId || model.JobAnnouncementId != jobAnnouncementId)
            {
                return BadRequest(new { success = false, message = "پارامترها همخوانی ندارند." });
            }

            var applicantExamAttemptViewModel = applicantExamAttemptService.Get(applicantExamAttemptId);


            var examQuestionViewModelList = examQuestionService.GetAllByExamId(applicantExamAttemptViewModel.ExamId);
            double totalGrade = 0;

            foreach (var examQuestionViewModel in examQuestionViewModelList)
            {
                var applicantExamQuestionAnswerViewModel = applicantExamQuestionAnswerService.Get(applicantExamAttemptViewModel.ApplicantExamAttemptId, examQuestionViewModel.ExamQuestionId);

                if (examQuestionViewModel.Type == 1)
                {
                    var examQuestionOptionViewModel = examQuestionViewModel.ExamQuestionOptionViewModelList.FirstOrDefault(x => x.IsCorrectAnswer);

                    if (examQuestionOptionViewModel.ExamQuestionOptionId == applicantExamQuestionAnswerViewModel.ExamQuestionOptionId)
                    {
                        applicantExamQuestionAnswerViewModel.Grade = examQuestionViewModel.Grade;
                    }
                    else
                    {
                        applicantExamQuestionAnswerViewModel.Grade = 0;
                    }

                    applicantExamQuestionAnswerService.Edit(applicantExamQuestionAnswerViewModel);
                    totalGrade += applicantExamQuestionAnswerViewModel.Grade;
                }

                if (examQuestionViewModel.Type == 2)
                {
                    foreach (var item in model.Corrections)
                    {
                        if (item.examQuestionId == applicantExamQuestionAnswerViewModel.ExamQuestionId)
                        {

                            applicantExamQuestionAnswerViewModel.AdditionalCorrectionTips = item.additionalCorrectionTips;
                            applicantExamQuestionAnswerViewModel.Grade = item.grade ?? 0;
                            applicantExamQuestionAnswerService.Edit(applicantExamQuestionAnswerViewModel);
                            totalGrade += applicantExamQuestionAnswerViewModel.Grade;
                        }
                    }
                }
            }


            applicantExamAttemptViewModel.FinalScore = (decimal)totalGrade;

            // محاسبه کل نمرات تستی و تشریحی و ذخیره قبول شدن یا رد شدن
            if (examQuestionViewModelList.All(x => x.Type != 2))
            {
                return Ok(new { success = true, message = "اطلاعات با موفقیت ذخیره شد." });
            }

            double allGrade = 0;
            foreach (var examQuestionViewModel in examQuestionViewModelList)
            {
                allGrade = examQuestionViewModel.Grade;
            }

            if (allGrade / 2 <= (double)applicantExamAttemptViewModel.FinalScore)
            {
                applicantExamAttemptViewModel.Status = 1;
            }
            else
            {
                applicantExamAttemptViewModel.Status = 2;
            }

            applicantExamAttemptService.Edit(applicantExamAttemptViewModel);

            // موفقیت — برگرداندن JSON به فرانت‌اند
            return Ok(new { success = true, message = "اطلاعات با موفقیت ذخیره شد." });
        }

    }
    public class ApplicantExamQuestionAndAnswerForAdminViewModel
    {
        public ApplicantExamQuestionAnswerViewModel ApplicantExamQuestionAnswerViewModel { get; set; }

        public ExamQuestionViewModel ExamQuestionViewModel { get; set; }
    }

    public class CorrectionRequestDto
    {
        public int JobAnnouncementId { get; set; }
        public int ApplicantExamAttemptId { get; set; }
        public List<CorrectionItem> Corrections { get; set; }
    }

    public class CorrectionItem
    {
        public int index { get; set; }
        public int examQuestionId { get; set; }
        public string additionalCorrectionTips { get; set; }
        public double? grade { get; set; }
    }
}