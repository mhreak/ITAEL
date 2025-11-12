using System;
using Web.Model;
using System.IO;
using System.Linq;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Web.Service.Identity.Interface;
using Microsoft.AspNetCore.Authorization;

[Area("Applicant")]
[Route("Applicant/[controller]")]
[Authorize(Roles = "Applicant,Admin")]
public class ExamController : BaseController
{
    private readonly string _storageFolder;
    private readonly IExamService _examService;
    private readonly IWebHostEnvironment _environment;
    private readonly IApplicantService _applicantService;
    private readonly IExamQuestionService _examQuestionService;
    private readonly IExamQuestionOptionService _examQuestionOptionService;
    private readonly IApplicantExamAttemptService _applicantExamAttemptService;
    private readonly IApplicationUserManagerService _applicationUserManagerService;
    private readonly IJobAnnouncement_Exam_Service _jobAnnouncement_Exam_Service;
    private readonly IApplicantExamQuestionAnswerService _applicantExamQuestionAnswerService;

    public ExamController(
        IExamService examService,
        IWebHostEnvironment environment,
        IApplicantService applicantService,
        IExamQuestionService examQuestionService,
        IJobAnnouncement_Exam_Service jobAnnouncement_Exam_Service,
        IExamQuestionOptionService examQuestionOptionService,
        IApplicantExamAttemptService applicantExamAttemptService,
        IApplicationUserManagerService applicationUserManagerService,
        IApplicantExamQuestionAnswerService applicantExamQuestionAnswerService)
    {
        _examService = examService;
        _environment = environment;
        _applicantService = applicantService;
        _examQuestionService = examQuestionService;
        _jobAnnouncement_Exam_Service = jobAnnouncement_Exam_Service;
        _examQuestionOptionService = examQuestionOptionService;
        _applicantExamAttemptService = applicantExamAttemptService;
        _applicationUserManagerService = applicationUserManagerService;
        _applicantExamQuestionAnswerService = applicantExamQuestionAnswerService;

        _storageFolder = Path.Combine(_environment.ContentRootPath, "Files", "ExamAttempts");
        Directory.CreateDirectory(_storageFolder);
    }

    [HttpGet]
    [Route("Index/{jobAnnouncementId}/{examId}")]
    public IActionResult Index(int jobAnnouncementId, int examId)
    {
        var jobExam = _jobAnnouncement_Exam_Service.Get(jobAnnouncementId, examId);
        if (jobExam == null)
        {
            ShowDangerToast(null, "اطلاعات موردنیاز یافت نشد.");
            return RedirectToAction("Index");
        }

        if (jobExam.StartTime > DateTime.Now || jobExam.EndTime < DateTime.Now)
        {
            ShowDangerToast(null, "باید در بازه زمانی آزمون اقدام کنید.");
            return RedirectToAction("Index");
        }

        var examQuestionList = _examQuestionService.GetAllByExamId(examId).ToList();
        Random random = new();

        if (jobExam.RandomizeOptions)
        {
            foreach (var q in examQuestionList)
                q.ExamQuestionOptionViewModelList = q.ExamQuestionOptionViewModelList.OrderBy(x => random.Next()).ToList();
        }

        var applicantId = _applicationUserManagerService.GetCurrentUser().ApplicantId.Value;

        var existingAttempt = _applicantExamAttemptService.GetByApplicantIdAndExamId(applicantId, examId);

        // اگر از قبل ساخته نشده باشه
        if (existingAttempt == null)
        {
            if (jobExam.RandomizeQuestions)
                examQuestionList = examQuestionList.OrderBy(x => random.Next()).ToList();

            // ساخت رشته csv از آیدی‌ها
            var orderCsv = string.Join(",", examQuestionList.Select(x => x.ExamQuestionId));

            var newAttempt = new ApplicantExamAttemptViewModel()
            {
                ApplicantId = applicantId,
                Status = 3,
                QuestionsOrder = orderCsv,
                ExamId = examId,
                FinalScore = 0,
                StartTime = DateTime.Now,
            };

            int newId = _applicantExamAttemptService.Add(newAttempt);
            if (newId == -1)
            {
                ShowDangerToast(null, "خطا در ذخیره اطلاعات");
                return RedirectToAction("Index", "Home");
            }
        }

        return View(jobExam);
    }

    [HttpGet]
    [Route("TakeExam/{examId}")]
    public IActionResult TakeExam(int? questionId, int examId)
    {
        var applicantId = _applicationUserManagerService.GetCurrentUser().ApplicantId.Value;
        var attempt = _applicantExamAttemptService.GetByApplicantIdAndExamId(applicantId, examId);
        if (attempt == null)
            return RedirectToAction("Index", "Home");

        // تبدیل CSV به لیست int
        var questionOrderList = attempt.QuestionsOrder?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.Parse(s.Trim()))
            .ToList() ?? new List<int>();

        int nextQuestionId;

        if (questionId == null)
        {
            if (!questionOrderList.Any())
                return RedirectToAction("FinishExam", "Exam");

            nextQuestionId = questionOrderList.First();
        }
        else
        {
            // پیدا کردن ایندکس سوال فعلی
            int curIndex = questionOrderList.IndexOf(questionId.Value);
            if (curIndex >= 0 && curIndex < questionOrderList.Count - 1)
            {
                nextQuestionId = questionOrderList[curIndex + 1];
            }
            else
            {
                // به پایان آزمون رسیدیم
                return RedirectToAction("FinishExam", "Exam");
            }
        }

        var examQuestionViewModel = _examQuestionService.Get(nextQuestionId);
        if (examQuestionViewModel == null)
        {
            ShowDangerToast(null, "سؤال پیدا نشد.");
            return RedirectToAction("FinishExam", "Exam");
        }

        return View(examQuestionViewModel); // View: نمایش فرم سوال با فرم POST به همین آدرس
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("TakeExam")]
    public IActionResult TakeExam(ApplicantExamQuestionAnswerViewModel model)
    {
        if (model == null)
        {
            ShowDangerToast(null, "اطلاعات ارسال نشده است.");
            return RedirectToAction("Index", "Home");
        }

        List<int> ids = [];
        // ذخیره پاسخ
        try
        {
            ids = _applicantExamQuestionAnswerService.Add(model);
            if (ids.Contains(-1))
                ShowDangerToast(null, "خطا هنگام ذخیره پاسخ.");
        }
        catch
        {
            ShowDangerToast(null, "خطا هنگام ذخیره پاسخ.");
            // برای خطا بهتر به همان سوال برگردان یا پیام خطا نمایش بده
            return RedirectToAction("TakeExam", new { questionId = model.ExamQuestionId });
        }

        // حالا باید سوال بعدی را پیدا کنیم و به آن ری‌دایرکت کنیم
        var applicantId = _applicationUserManagerService.GetCurrentUser().ApplicantId.Value;
        var attempt = _applicantExamAttemptService.GetByApplicantIdAndExamId(applicantId, ids[0]);
        var order = attempt.QuestionsOrder
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s.Trim()))
                    .ToList();

        int curIndex = order.IndexOf(model.ExamQuestionId);
        if (curIndex >= 0 && curIndex < order.Count - 1)
        {
            int nextQuestionId = order[curIndex + 1];
            return RedirectToAction("TakeExam", new { questionId = nextQuestionId });
        }
        else
        {
            // پایان
            return RedirectToAction("FinishExam", "Exam");
        }
    }

    [Route("FinishExam")]
    public IActionResult FinishExam()
    {
        return View();
    }

}
