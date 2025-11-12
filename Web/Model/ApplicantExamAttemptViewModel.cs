using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class ApplicantExamAttemptViewModel
    {
        [Display(Name = "شناسه تلاش کاربر در آزمون")]
        public int ApplicantExamAttemptId { get; set; }

        [Display(Name = "شناسه داوطلب")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int ApplicantId { get; set; }

        [Display(Name = "نام داوطلب")]
        public string? ApplicantFullName { get; set; }

        [Display(Name = "شناسه آزمون")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int ExamId { get; set; }

        [Display(Name = "عنوان آزمون")]
        public string? ExamTitle { get; set; }

        [Display(Name = "تاریخ شروع آزمون")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime StartTime { get; set; }

        [Display(Name = "تاریخ شروع آزمون")]
        public string? ShamsiStartTime { get; set; }

        [Display(Name = "تاریخ پایان آزمون")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "تاریخ پایان آزمون")]
        public string? ShamsiEndTime { get; set; }

        [Display(Name = "نمره پایانی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public decimal FinalScore { get; set; }

        //فقط برای زمانی که سوالات رندوم باشد.
        public string? QuestionsOrder { get; set; }

        //1 -> قبول شده
        //2 -> رد شده
        //3 -> در حال بررسی
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public short Status { get; set; }

        [Display(Name = "وضعیت")]
        public string? StatusStr { get; set; }
    }
}
