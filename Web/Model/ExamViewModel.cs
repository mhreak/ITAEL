using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class ExamViewModel
    {
        [Display(Name = "شناسه آزمون")]
        public int ExamId { get; set; }

        [MaxLength(200)]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Title { get; set; }

        [MaxLength(2000)]
        [Display(Name = "جزئیات")]
        public string? Description { get; set; }

        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime StartTime { get; set; }

        [Display(Name = "تاریخ شروع")]
        public string? ShamsiStartTime { get; set; }

        [Display(Name = "تاریخ پایان")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime EndTime { get; set; }

        [Display(Name = "تاریخ شروع")]
        public string? ShamsiEndTime { get; set; }

        [Display(Name = "طول آزمون (دقیقه)")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int DurationMinutes { get; set; }

        //سوالات رندوم
        [Display(Name = "ترتیب رندوم سوالات")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool RandomizeQuestions { get; set; }

        [Display(Name = "ترتیب رندوم سوالات")]
        public string? RandomizeQuestionsStr { get; set; }

        //جواب های رندوم
        [Display(Name = "ترتیب رندوم گزینه ها")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool RandomizeOptions { get; set; }

        [Display(Name = "ترتیب رندوم گزینه ها")]
        public string? RandomizeOptionsStr { get; set; }

        //برگشتن به سؤال قبل
        [Display(Name = "اجازه برگشتن به سوال قبل")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool AllowNavigateToPreviousQuestion { get; set; }

        [Display(Name = "اجازه برگشتن به سوال قبل")]
        public string? AllowNavigateToPreviousQuestionStr { get; set; }
    }
}
