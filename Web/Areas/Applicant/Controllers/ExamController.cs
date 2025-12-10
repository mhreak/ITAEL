using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;

using Web.Areas.Admin.Controllers;
using Web.Controllers;
using Web.Model;
using Web.Service.Identity.Interface;
using Web.Service.Interface;

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
    private readonly IJobAnnouncementService _jobAnnouncementService;
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
        IJobAnnouncementService jobAnnouncementService,
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
        _jobAnnouncementService = jobAnnouncementService;
        _jobAnnouncement_Exam_Service = jobAnnouncement_Exam_Service;
        _examQuestionOptionService = examQuestionOptionService;
        _applicantExamAttemptService = applicantExamAttemptService;
        _applicationUserManagerService = applicationUserManagerService;
        _applicantExamQuestionAnswerService = applicantExamQuestionAnswerService;

        _storageFolder = Path.Combine(_environment.ContentRootPath, "Files", "ExamAttempts");
        Directory.CreateDirectory(_storageFolder);
    }

    [HttpGet]
    [Route("Index/{jobAnnouncementId}")]
    public IActionResult Index(int jobAnnouncementId)
    {
        var ja_Exam_ViewModelList = _jobAnnouncement_Exam_Service.GetAllByJobAnnouncementId(jobAnnouncementId).Where(x => x.StartTime <= DateTime.Now && x.EndTime >= DateTime.Now);
        var examIndexViewModelList = new List<ExamIndexViewModel>();
        var user = _applicationUserManagerService.GetCurrentUser();
        var applicant = _applicantService.Get(user.ApplicantId.Value);
        if (ja_Exam_ViewModelList != null && ja_Exam_ViewModelList.Any())
        {
            foreach (var ja_Exam_ViewModel in ja_Exam_ViewModelList)
            {
                var examViewModel = _examService.Get(ja_Exam_ViewModel.ExamId);

                var applicantExamAttemptViewModel = _applicantExamAttemptService.GetByApplicantIdAndExamId(applicant.ApplicantId, examViewModel.ExamId);

                var examIndexViewModel = new ExamIndexViewModel() { ExamViewModel = examViewModel };

                if (applicantExamAttemptViewModel != null)
                {
                    if (applicantExamAttemptViewModel.Status is 1 or 2 or 3)
                    {
                        examIndexViewModel.State = 2;
                    }
                    else if (applicantExamAttemptViewModel.Status is 4)
                    {
                        examIndexViewModel.State = 1;
                    }
                    else
                    {
                        examIndexViewModel.State = 3;
                    }
                }
                else
                {
                    examIndexViewModel.State = 3;
                }

                examIndexViewModelList.Add(examIndexViewModel);
            }
        }

        ViewBag.JobAnnouncementId = jobAnnouncementId;
        return View(examIndexViewModelList);
    }

    [HttpGet]
    [Route("Details/{jobAnnouncementId}/{examId}")]
    public IActionResult Details(int jobAnnouncementId, int examId)
    {
        var jobExam = _jobAnnouncement_Exam_Service.Get(jobAnnouncementId, examId);
        if (jobExam == null)
        {
            TempData["ErrorMessage"] = "اطلاعات موردنیاز یافت نشد.";
            return RedirectToAction("Index", "Exam", new { Area = "Applicant", jobAnnouncementId });
        }

        if (jobExam.StartTime > DateTime.Now || jobExam.EndTime < DateTime.Now)
        {
            TempData["ErrorMessage"] = "باید در بازه زمانی آزمون اقدام کنید.";
            return RedirectToAction("Index", "Exam", new { Area = "Applicant", jobAnnouncementId });

        }

        var examQuestionList = _examQuestionService.GetAllByExamId(examId).ToList();

        var examDetailsViewModel = new ExamDetailsViewModel()
        {
            ExamId = jobExam.ExamId,
            JobAnnouncementId = jobAnnouncementId,
            ExamTitle = jobExam.ExamTitle,
            StartDate = ConvertToShamsiDate(jobExam.StartTime),
            StartDateTime = jobExam.ShamsiStartTime,
            EndDate = ConvertToShamsiDate(jobExam.EndTime),
            EndDateTime = jobExam.ShamsiEndTime,
            DurationMinutes = jobExam.DurationMinutes,
            QuestionsCount = examQuestionList.Count,
            MultipleChoiceQuestionsCount = examQuestionList.Count(x => x.Type == 1),
            AnnotationQuestionsCount = examQuestionList.Count(x => x.Type == 2)
        };

        return View(examDetailsViewModel);
    }

    [HttpGet]
    [Route("ExamResult/{jobAnnouncementId}/{examId}")]
    public IActionResult ExamResult(int jobAnnouncementId, int examId)
    {
        var user = _applicationUserManagerService.GetCurrentUser();

        var applicantViewModel = _applicantService.Get(user.ApplicantId.Value);

        var examViewModel = _examService.Get(examId);

        var jobAnnouncementViewModel = _jobAnnouncementService.Get(jobAnnouncementId);

        var applicantExamAttemptViewModel = _applicantExamAttemptService.GetByApplicantIdAndExamId(applicantViewModel.ApplicantId, examViewModel.ExamId);

        var examQuestionViewModelList = _examQuestionService.GetAllByExamId(examViewModel.ExamId);

        var dataList = new List<ApplicantExamQuestionAndAnswerForApplicantViewModel>();

        foreach (var examQuestionViewModel in examQuestionViewModelList)
        {
            var applicantExamQuestionAnswerViewModel = _applicantExamQuestionAnswerService.Get(applicantExamAttemptViewModel.ApplicantExamAttemptId,
                                                                                              examQuestionViewModel.ExamQuestionId);

            var data = new ApplicantExamQuestionAndAnswerForApplicantViewModel()
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
        ViewBag.StatusStr = applicantExamAttemptViewModel.StatusStr;
        return View(dataList);
    }

    [HttpGet]
    [Route("TakeExam/{jobAnnouncementId:int}/{examId:int}/{questionId?}")]
    public IActionResult TakeExam(int jobAnnouncementId, int examId, int? questionId)
    {
        var jobExam = _jobAnnouncement_Exam_Service.Get(jobAnnouncementId, examId);
        var examQuestionList = _examQuestionService.GetAllByExamId(examId).ToList();
        var applicantId = _applicationUserManagerService.GetCurrentUser().ApplicantId.Value;
        var existingAttempt = _applicantExamAttemptService.GetByApplicantIdAndExamId(applicantId, examId);

        Random random = new();

        if (jobExam.RandomizeOptions)
        {
            foreach (var q in examQuestionList)
            {
                q.ExamQuestionOptionViewModelList = q.ExamQuestionOptionViewModelList.OrderBy(x => random.Next()).ToList();
            }
        }

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
                Status = 5,
                QuestionsOrder = orderCsv,
                ExamId = examId,
                FinalScore = 0,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(jobExam.DurationMinutes)
            };

            int newId = _applicantExamAttemptService.Add(newAttempt);
            if (newId == -1)
            {
                TempData["ErrorMessage"] = "لطفا دوباره نلاش کنید.";
                return RedirectToAction("Index", "Exam", new { Area = "Applicant", jobAnnouncementId = jobAnnouncementId });
            }

            existingAttempt = _applicantExamAttemptService.GetByApplicantIdAndExamId(applicantId, examId);
        }
        else
        {
            if (existingAttempt.Status is 1 or 2 or 3 or 4)
            {
                TempData["ErrorMessage"] = "شما قبلا این آزمون را به پایان رساندید.";
                return RedirectToAction("Index", "Exam", new { Area = "Applicant", jobAnnouncementId = jobAnnouncementId });
            }
        }

        var questionOrderList = existingAttempt.QuestionsOrder?
                                               .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                               .Select(s => int.Parse(s.Trim()))
                                               .ToList() ?? new List<int>();

        if (!questionOrderList.Any())
        {
            TempData["ErrorMessage"] = "سؤالی برای این آزمون تعریف نشده است.";
            return RedirectToAction("Index", "Exam", new { Area = "Applicant", jobAnnouncementId = jobAnnouncementId });
        }

        int currentQuestionId;
        int currentIndex;
        if (questionId == null)
        {
            currentQuestionId = questionOrderList.First();
            currentIndex = 1;
        }
        else
        {
            currentQuestionId = questionId.Value;
            currentIndex = questionOrderList.IndexOf(currentQuestionId) + 1;
            if (currentIndex == 0)
            {
                TempData["ErrorMessage"] = "سؤال معتبر نیست.";
                return RedirectToAction("Index", "Exam", new { Area = "Applicant", jobAnnouncementId = jobAnnouncementId });
            }
        }

        var examQuestionViewModel = _examQuestionService.Get(currentQuestionId);
        if (examQuestionViewModel == null)
        {
            TempData["ErrorMessage"] = "سؤال پیدا نشد.";
            return RedirectToAction("Index", "Exam", new { Area = "Applicant", jobAnnouncementId = jobAnnouncementId });
        }

        var existingAnswer = _applicantExamQuestionAnswerService.Get(existingAttempt.ApplicantExamAttemptId, currentQuestionId);

        var jaExamViewModel = _jobAnnouncement_Exam_Service.Get(jobAnnouncementId, examId);
        if (jaExamViewModel == null)
        {
            TempData["ErrorMessage"] = "اطلاعات آزمون یافت نشد.";
            return RedirectToAction("Index", "Exam", new { Area = "Applicant", jobAnnouncementId = jobAnnouncementId });
        }

        var vm = new SubmitAnswerViewModel
        {
            JobAnnouncementId = jobAnnouncementId,
            ExamId = examId,
            ExamQuestionId = currentQuestionId,
            ExamQuestionViewModel = examQuestionViewModel,
            SelectedOptionId = existingAnswer?.ExamQuestionOptionId,
            AnswerText = existingAnswer?.AnswerText,
            AllowNavigateToPreviousQuestion = jaExamViewModel.AllowNavigateToPreviousQuestion,
            QuestionsCount = questionOrderList.Count,
            CurrentIndex = currentIndex,
            ApplicantExamAttemptId = existingAttempt.ApplicantExamAttemptId,
            PrevQuestionId = currentIndex > 1 ? questionOrderList[currentIndex - 2] : (int?)null,
            NextQuestionId = currentIndex < questionOrderList.Count ? questionOrderList[currentIndex] : (int?)null
        };

        ViewBag.JobAnnouncementId = jobAnnouncementId;
        ViewBag.DurationMinutes = jaExamViewModel.DurationMinutes;

        DateTime endUtc;
        if (existingAttempt.EndTime != default(DateTime))
            endUtc = existingAttempt.EndTime.Value.ToUniversalTime();
        else
            endUtc = (existingAttempt.StartTime == default(DateTime) ? DateTime.UtcNow : existingAttempt.StartTime.ToUniversalTime()).AddMinutes(jaExamViewModel.DurationMinutes);

        ViewBag.EndTimeUtc = endUtc.ToString("o");

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("TakeExam/{jobAnnouncementId:int}/{examId:int}/{questionId?}")]
    public IActionResult TakeExam(SubmitAnswerViewModel model, string NavigationAction)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model == null)
        {
            TempData["ErrorMessage"] = "اطلاعات ارسال نشده است.";
            return View(model);
        }

        var applicantId = _applicationUserManagerService.GetCurrentUser().ApplicantId ?? 0;
        var attempt = _applicantExamAttemptService.GetByApplicantIdAndExamId(applicantId, model.ExamId);
        if (attempt == null)
        {
            TempData["ErrorMessage"] = "امکان ذخیره پاسخ وجود ندارد.";
            return View(model);
        }

        if (attempt.Status is 1 or 2 or 3 or 4)
        {
            TempData["ErrorMessage"] = "شما قبلا این آزمون را به پایان رساندی";
            return View(model);
        }

        var order = attempt.QuestionsOrder?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.Parse(s.Trim()))
            .ToList() ?? new List<int>();

        if (!order.Contains(model.ExamQuestionId))
        {
            TempData["ErrorMessage"] = "سؤال ارسالی معتبر نیست.";
            return View(model);
        }

        try
        {

            if (attempt.StartTime <= DateTime.Now && attempt.EndTime >= DateTime.Now)
            {
                var existing = _applicantExamQuestionAnswerService.Get(attempt.ApplicantExamAttemptId, model.ExamQuestionId);
                if (existing == null)
                {
                    var newAnswer = new ApplicantExamQuestionAnswerViewModel
                    {
                        ApplicantExamAttemptId = attempt.ApplicantExamAttemptId,
                        ExamQuestionId = model.ExamQuestionId,
                        AnswerText = model.AnswerText,
                        ExamQuestionOptionId = model.SelectedOptionId,
                        InsertDate = DateTime.Now
                    };

                    _applicantExamQuestionAnswerService.Add(newAnswer);
                }
                else
                {
                    existing.AnswerText = model.AnswerText;
                    existing.ExamQuestionOptionId = model.SelectedOptionId;
                    existing.InsertDate = DateTime.Now;
                    _applicantExamQuestionAnswerService.Edit(existing);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "زمان آزمون به پایان رسیده است آزمون با موفقیت ثبت شد.";
                _applicantExamQuestionAnswerService.CalculateGradeAfterExam(model.ExamId, applicantId);
                return RedirectToAction("Index", "Dashboard", new { Area = "Applicant" });
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "خطا لطفا مجدد تلاش کنید.";
            return RedirectToAction("TakeExam", new { jobAnnouncementId = model.JobAnnouncementId, examId = model.ExamId, questionId = model.ExamQuestionId });
        }

        var curIndex = order.IndexOf(model.ExamQuestionId);
        if (!string.IsNullOrEmpty(NavigationAction) && NavigationAction.Equals("prev", StringComparison.OrdinalIgnoreCase))
        {
            if (curIndex > 0)
            {
                var prevQuestionId = order[curIndex - 1];
                return RedirectToAction("TakeExam", new { jobAnnouncementId = model.JobAnnouncementId, examId = model.ExamId, questionId = prevQuestionId });
            }
            else
            {
                return RedirectToAction("TakeExam", new { jobAnnouncementId = model.JobAnnouncementId, examId = model.ExamId, questionId = model.ExamQuestionId });
            }
        }

        if (!string.IsNullOrEmpty(NavigationAction) && NavigationAction.Equals("finish", StringComparison.OrdinalIgnoreCase))
        {
            TempData["SuccessMessage"] = "آزمون با موفقیت به پایان رسید.";
            _applicantExamQuestionAnswerService.CalculateGradeAfterExam(model.ExamId, applicantId);
            return RedirectToAction("Index", "Dashboard", new { Area = "Applicant" });
        }

        if (curIndex >= 0 && curIndex < order.Count - 1)
        {
            var nextQuestionId = order[curIndex + 1];
            return RedirectToAction("TakeExam", new { jobAnnouncementId = model.JobAnnouncementId, examId = model.ExamId, questionId = nextQuestionId });
        }

        TempData["SuccessMessage"] = "آزمون با موفقیت به پایان رسید.";
        _applicantExamQuestionAnswerService.CalculateGradeAfterExam(model.ExamId, applicantId);
        return RedirectToAction("Index", "Dashboard", new { Area = "Applicant" });
    }

    //[HttpGet]
    //[Route("FinishExam/{examId}")]
    //public IActionResult FinishExam(int examId)
    //{
    //    var examViewModel = _examService.Get(examId);

    //    if (examViewModel == null)
    //    {
    //        TempData["ErrorMessage"] = "خطا لطفا مجدد تلاش کنید.";

    //        return View();
    //    }

    //    var applicantId = _applicationUserManagerService.GetCurrentUser().ApplicantId ?? 0;
    //    var attempt = _applicantExamAttemptService.GetByApplicantIdAndExamId(applicantId, examId);
    //    if (attempt == null)
    //    {
    //        TempData["ErrorMessage"] = "خطا لطفا مجدد تلاش کنید.";
    //        return RedirectToAction("Index", "Home");
    //    }

    //    if (attempt.Status is 1 or 2 or 3 or 4)
    //    {
    //        TempData["ErrorMessage"] = "شما قبلا این آزمون را به پایان رساندی.";
    //        return RedirectToAction("Index", "Exam", new { Area = "Applicant" });
    //    }


    //    return View(examViewModel);
    //}

    //[HttpPost]
    //[Route("FinishExam/{examId}")]
    //public IActionResult FinishExam(ExamViewModel model)
    //{
    //    var examViewModel = _examService.Get(model.ExamId);

    //    if (examViewModel == null)
    //    {
    //        TempData["ErrorMessage"] = "خطا لطفا مجدد تلاش کنید.";

    //        return View();
    //    }

    //    var applicantId = _applicationUserManagerService.GetCurrentUser().ApplicantId ?? 0;
    //    var attempt = _applicantExamAttemptService.GetByApplicantIdAndExamId(applicantId, examViewModel.ExamId);
    //    if (attempt == null)
    //    {
    //        TempData["ErrorMessage"] = "خطا لطفا مجدد تلاش کنید.";
    //        return RedirectToAction("Index", "Home");
    //    }

    //    if (attempt.Status is 1 or 2 or 3 or 4)
    //    {
    //        TempData["ErrorMessage"] = "شما قبلا این آزمون را به پایان رساندی.";
    //        return RedirectToAction("Index", "Exam", new { Area = "Applicant" });
    //    }


    //    bool isCalc = _applicantExamQuestionAnswerService.CalculateGradeAfterExam(examViewModel.ExamId, applicantId);

    //    if (!isCalc)
    //    {
    //        TempData["ErrorMessage"] = "خطا لطفا مجدد تلاش کنید.";
    //        return RedirectToAction("Index", "Home");
    //    }

    //    return RedirectToAction("Index", "Dashboard", new { Area = "Applicant" });
    //}

    [HttpGet]
    [Route("GetQuestionOptions/{examId}/{questionId}")]
    public IActionResult GetQuestionOptions(int examId, int questionId)
    {
        var options = _examQuestionOptionService.GetAllByExamQuestionId(questionId);

        if (options == null)
            return NotFound();

        return Ok(options);
    }

    private string ConvertToShamsiDate(DateTime? date)
    {
        if (date == null) return null;

        try
        {
            if (date.Value < new DateTime(622, 3, 22) || date.Value > new DateTime(9999, 12, 31))
                return null; // یا می‌تونی پیام "نامعتبر" برگردونی

            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetYear(date.Value)}/{pc.GetMonth(date.Value):00}/{pc.GetDayOfMonth(date.Value):00}";
        }
        catch
        {
            return null; // یا "تاریخ نامعتبر"
        }
    }
}

public class ExamIndexViewModel
{
    public ExamViewModel ExamViewModel { get; set; }

    //1 -> پایان یافته ولی توسط مصحح بررسی نشده
    //2 -> پایان یافته و توسط مصحح بررسی شده
    //3 -> پایان نیافته یا شروع نشده
    public short State { get; set; }
}

public class ExamDetailsViewModel
{
    public int ExamId { get; set; }

    public int JobAnnouncementId { get; set; }

    public string ExamTitle { get; set; }

    public string StartDate { get; set; }

    public string StartDateTime { get; set; }

    public string EndDate { get; set; }

    public string EndDateTime { get; set; }

    public int DurationMinutes { get; set; }

    public int QuestionsCount { get; set; }

    public int MultipleChoiceQuestionsCount { get; set; }

    public int AnnotationQuestionsCount { get; set; }
}

public class SubmitAnswerViewModel
{
    public int JobAnnouncementId { get; set; }
    public int ExamId { get; set; }
    public int ExamQuestionId { get; set; }

    public int? SelectedOptionId { get; set; }

    public string? AnswerText { get; set; }

    public ExamQuestionViewModel? ExamQuestionViewModel { get; set; }

    public int? PrevQuestionId { get; set; }
    public int? NextQuestionId { get; set; }
    public bool AllowNavigateToPreviousQuestion { get; set; }
    public int QuestionsCount { get; set; }
    public int CurrentIndex { get; set; }
    public int? ApplicantExamAttemptId { get; set; }
}


public class ApplicantExamQuestionAndAnswerForApplicantViewModel
{
    public ApplicantExamQuestionAnswerViewModel ApplicantExamQuestionAnswerViewModel { get; set; }

    public ExamQuestionViewModel ExamQuestionViewModel { get; set; }
}