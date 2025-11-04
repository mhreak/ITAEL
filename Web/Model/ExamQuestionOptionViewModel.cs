using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class ExamQuestionOptionViewModel
    {
        [Display(Name = "شناسه گزینه سوال آزمون")]
        public int ExamQuestionOptionId { get; set; }

        [MaxLength(200)]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Title { get; set; }

        [Display(Name = "شناسه سوال آزمون")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int ExamQuestionId { get; set; }

        [Display(Name = "ترتیب")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public short Order { get; set; }

        [Display(Name = "آیا جواب درست است؟")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool IsCorrectAnswer { get; set; }

        [Display(Name = "آیا جواب درست است؟")]
        public string? IsCorrectAnswerStr { get; set; }
    }
}
