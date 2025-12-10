using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class ApplicantExamQuestionAnswerViewModel
    {
        [Display(Name = " شناسه تلاش کاربر در آزمون")]
        public int ApplicantExamAttemptId { get; set; }

        [Display(Name = "شناسه سوال آزمون")]
        public int ExamQuestionId { get; set; }

        [Display(Name = "سوال آزمون")]
        public string? ExamQuestionText { get; set; }

        [Display(Name = "متن جواب")]
        [MaxLength(4000)]
        public string? AnswerText { get; set; }

        [Display(Name = "شناسه گزینه سوال آزمون")]
        public int? ExamQuestionOptionId { get; set; }

        public double Grade { get; set; }

        [Display(Name = "نکات تکمیلی مصحح")]
        [MaxLength(4000)]
        public string? AdditionalCorrectionTips { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string? ShamsiInsertDate { get; set; }
    }
}
